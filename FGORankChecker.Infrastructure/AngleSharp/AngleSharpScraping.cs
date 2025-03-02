using AngleSharp.Html.Parser;
using AngleSharp.Html.Dom;
using FGORankChecker.Domain.Scraping;
using FGORankChecker.Domain.Entities;
using FGORankChecker.Domain.Exceptions;
using AngleSharp.Text;

namespace FGORankChecker.Infrastructure.AngleSharp
{
    public class AngleSharpScraping : IServantScraping
    {
        // 最強サーヴァントランキング
        private const string URL = "https://appmedia.jp/fategrandorder/1351236";

        // HttpClientは1つを使い回す必要がある
        private static readonly HttpClient _httpClient = new() { Timeout = TimeSpan.FromSeconds(10) };

        public List<ServantEntity> GetServantList()
        {
            var servantEntities = new List<ServantEntity>();
            try
            {

                // AppMediaのHTML取得
                IHtmlDocument? appMediaDoc = GetParseHtml(URL).Result ?? throw new ScrapingException("HTMLの取得に失敗しました");

                // サーヴァントランキングtbody取得
                var servantRankTrs = appMediaDoc.QuerySelectorAll("#fgo_servant_ranking_list_new > table > tbody > tr");
                if (servantRankTrs.Length == 0)
                {
                    throw new ScrapingException("#fgo_servant_ranking_list_new > table > tbody > trの取得に失敗しました");
                }

                // tr内のthのdata-val属性を取得
                var servantRankTrCount = servantRankTrs.Length;
                var servantRank = "";
                for (int i = 0; i <= servantRankTrCount; i++)
                {
                    // 偶数の場合はランクラベル
                    if (i % 2 == 0)
                    {
                        var servantRankTh = servantRankTrs[i].QuerySelector("th") ?? throw new ScrapingException("thの取得に失敗しました");
                        var label = servantRankTh.GetAttribute("data-val") ?? throw new ScrapingException("data-valの取得に失敗しました");
                        servantRank = label;
                    }
                    // 奇数の場合はServant一覧
                    else
                    {
                        var servantLis = servantRankTrs[i].QuerySelectorAll("td > ul > li") ?? throw new ScrapingException("td > ul > liの取得に失敗しました");
                        if (servantLis.Length == 0)
                        {
                            throw new ScrapingException("td > ul > liの取得に失敗しました");
                        }

                        // サーヴァント名取得
                        foreach (var servantLi in servantLis)
                        {
                            var aTag = servantLi.QuerySelector("a") ?? throw new ScrapingException("aの取得に失敗しました");
                            var id = Convert.ToInt32(aTag.GetAttribute("href")?.Split('/').Last());
                            var imgTag = aTag.QuerySelector("img") ?? throw new ScrapingException("imgの取得に失敗しました");
                            var name = imgTag.GetAttribute("alt") ?? "";
                            var thumbnail = imgTag.GetAttribute("src") ?? "";
                            servantEntities.Add(
                                new ServantEntity(
                                    id: id,
                                    name: name,
                                    rank: servantRank,
                                    rare: servantLi.GetAttribute("data-rarity") ?? "",
                                    classType: servantLi.GetAttribute("data-class_2") ?? "",
                                    npType: servantLi.GetAttribute("data-type_2") ?? "",
                                    range: servantLi.GetAttribute("data-range") ?? "",
                                    thumbnail: thumbnail
                                )
                            );
                        }
                    }
                }
                return servantEntities;
            }
            catch
            {
                return servantEntities;
            }
        }

        private async Task<IHtmlDocument?> GetParseHtml(string url)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string contents = await response.Content.ReadAsStringAsync();

                // HTMLパース
                var parser = new HtmlParser();
                return parser.ParseDocument(contents);
            }
            catch
            {
                return null;
            }
        }
    }
}
