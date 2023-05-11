using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MultiChatServer
{
    internal class MenuRecommend
    {
        static HttpClient client = new HttpClient();
        string url = "http://apis.data.go.kr/1360000/VilageFcstInfoService_2.0/getUltraSrtNcst"; // URL
        string results = string.Empty;
        double tmp = 0;
        double precipitation = 0;
        string[] menu = {"족발", "보쌈", "막국수", "불족발", "부대찌개", "냉채족발", "부추전", "김치전", "까만찜닭", 
            "빨간찜닭", "로제찜닭", "코다리조림", "꼬막비빔밥", "쭈꾸미정식", "곰탕", "우거지탕", "해물찜", "아구찜", 
            "알곤찜", "대구뽈찜", "꽃게찜", "김치찜", "갈비찜", "설렁탕", "삼계탕", "갈비탕", "육회비빔밥", "함흥냉면", 
            "평양냉면", "회덮밥", "연어덮밥", "모둠초밥", "회전초밥", "대구탕", "알탕", "돈코츠라멘", "미소라멘", "소유라멘", 
            "장어덮밥", "규동(소고기덮밥)", "치킨 마요 덮밥", "삼겹살구이 덮밥", "조개탕", "나가사키 짬뽕탕", "오뎅탕", 
            "김치치즈가츠나베", "가츠동", "카레 돈가스", "등심 돈가스", "텐동", "스테이크 덮밥", "쭈꾸미 덮밥", "낚지 덮밥", 
            "미역국", "죽", "피자", "제육볶음", "떡볶이", "로제 떡볶이", "알리오올리오", "로제 파스타", "크림 파스타",  
            "후라이드 치킨", "양념치킨", "간장치킨", "짜장면", "짬뽕", "쟁반짜장", "간짜장", "쌀국수", "샤브샤브", 
            "우삼겹 덮밥", "스팸김치볶음밥", "소불고기 도시락", "닭갈비 도시락", "순두부찌개", "김치찌개", "고등어 구이", 
            "감자전", "마늘족발", "쪽갈비", "샌드위치", "햄버거", "서브웨이", "수제버거", "이삭토스트", "회", 
            "콩나물국밥", "순대국밥", "수육국밥", "쭈꾸미삼겹 덮밥", "토마토 파스타", "샐러드", "오꼬노미야끼", "육회 덮밥"};

        public MenuRecommend()
        {
            url += "?ServiceKey=" + "pLBMhaAKgs5d%2FhcjmoKmTh9VEIKcq948xlXdnVVv1hwIwjc%2FBQZqHcb1c6SZ2spb%2Br%2B89WN6SjL8gggy2HkwqQ%3D%3D"; // Service Key
            url += "&pageNo=1";
            url += "&numOfRows=1000";
            url += "&dataType=XML";
            url += "&base_date=20230510"; //당일 날짜
            url += "&base_time=1200"; //시간
            url += "&nx=62"; //경기도 성남시 시흥동의 경우 격자 X는 62, 격자 Y는 124
            url += "&ny=124";

            Console.WriteLine(url);

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            HttpWebResponse response;
            using (response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                results = reader.ReadToEnd();
            }

            Console.WriteLine(results);
            //PTY는 강수형태, REH는 습도, RN1는 1시간 강수량, T1H는 기온
            //UUU는 풍속(동서성분), VEC는 풍향, VVV는 풍속(남북성분), WSD는 풍속


            // 수신된 XML형식의 데이터를 컨트롤하기위해 XmlDocument 인스턴스를 생성
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(results);   // Stream결과를 XML형식으로 읽어오기
            XmlNodeList xmResponse = xml.GetElementsByTagName("response");  // <response></response> 기준으로 node 생성
            XmlNodeList xmlList = xml.GetElementsByTagName("item"); // <item></item> 기준으로 node 생성

            foreach (XmlNode node in xmResponse)    // xml의 <response> 값 읽어 들이기
            {
                if (node["header"]["resultMsg"].InnerText.Equals("NORMAL_SERVICE")) // 정상 응답일 경우
                {
                    foreach (XmlNode node1 in xmlList)  // <item> 값 읽어 들이기
                    {
                        if (node1["category"].InnerText.Equals("T1H"))  // 기온
                        {
                            tmp = double.Parse(node1["obsrValue"].InnerText);
                            Console.WriteLine("{0}", tmp);
                        }

                        if (node1["category"].InnerText.Equals("PTY"))  // 강수
                        {
                            precipitation = double.Parse(node1["obsrValue"].InnerText);
                            Console.WriteLine("{0}",precipitation);
                            /*
                            switch (double.Parse(node1["obsrValue"].InnerText))
                            {
                                case 0:
                                    Console.WriteLine("없음");
                                    break;
                                case 1:
                                    Console.WriteLine("비");
                                    break;
                                default:
                                    Console.WriteLine("해당 자료가 없습니다.");
                                    break;
                            }
                            */
                        }
                    }
                }
            }


        }

        ~MenuRecommend()
        {

        }

        public void Recommend()
        {
            
        }

    }
}
