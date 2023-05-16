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
        string url = "http://apis.data.go.kr/1360000/VilageFcstInfoService_2.0/getUltraSrtNcst"; // URL, getUltraSrtNcst는 초단기실황조회
        string results = string.Empty;
        public string menu_result = string.Empty;
        public double tmp = 0; //기온
        public int precipitation = 0; //강수형태
        public int humidity = 0; //습도
        string[] all_menu = new string[] {"부대찌개", "순두부찌개", "김치찌개", "돈코츠라멘", "미소라멘", "소유라멘", "김치카츠나베",
            "곰탕", "우거지탕", "설렁탕", "나가사키짬뽕", "해물짬뽕", "쌀국수", "샤브샤브", "해물탕", "알탕", "조개탕", "감자탕", "오뎅탕", "삼계탕",
            "갈비탕", "콩나물국밥", "순대국", "수육국밥", "해장국", "밀푀유나베", "우동", "해물파전", "김치전", "감자전", "칼국수", "수제비",
            "회덮밥", "연어덮밥", "육회비빔밥", "함흥냉면", "평양냉면", "막국수", "모둠초밥", "회전초밥", "회", "샐러드", "포케", "족발",
            "불족발", "보쌈", "간장찜닭", "매운찜닭", "로제찜닭", "쟁반짜장", "간짜장", "탕수육", "꿔바로우", "쭈꾸미볶음", "오징어볶음",
            "제육볶음", "해물찜", "아구찜", "알곤찜", "대구뽈찜", "꽃게찜", "김치찜", "갈비찜", "마라탕", "오꼬노미야끼", "커리", "장어덮밥",
            "규동", "삼겹살덮밥", "가츠동", "텐동", "스테이크덮밥", "쭈꾸미덮밥", "김치볶음밥", "꼬막비빔밥", "양장피", "유산슬", "국수",
            "카레돈가스", "등심돈가스", "피자", "서브웨이", "스테이크", "떡볶이", "로제떡볶이", "알리오올리오", "로제파스타", "크림파스타",
            "토마토파스타", "후라이드치킨", "양념치킨", "간장치킨", "생선구이", "코다리조림", "쪽갈비", "삼겹살", "양념갈비", "한우",
            "샌드위치", "햄버거"};
        string[] hot_menu = new string[] { "부대찌개", "순두부찌개", "김치찌개", "돈코츠라멘", "미소라멘", "소유라멘", "김치카츠나베",
            "곰탕", "우거지탕", "설렁탕", "나가사키짬뽕", "해물짬뽕", "쌀국수", "샤브샤브", "해물탕", "알탕", "조개탕", "감자탕",
            "오뎅탕", "삼계탕", "갈비탕", "콩나물국밥", "순대국", "수육국밥", "해장국", "밀푀유나베", "우동"};
        string[] rain_menu = new string[] { "해물파전", "김치전", "감자전", "칼국수", "수제비" };
        string[] cold_menu = new string[] { "회덮밥", "연어덮밥", "육회비빔밥", "함흥냉면", "평양냉면", "막국수", "모둠초밥", "회전초밥", "회", "샐러드", "포케" };
        string[] else_menu = new string[] { "족발", "불족발", "보쌈", "간장찜닭", "매운찜닭", "로제찜닭", "쟁반짜장", "간짜장", "탕수육",
            "꿔바로우", "쭈꾸미볶음", "오징어볶음", "제육볶음", "해물찜", "아구찜", "알곤찜", "대구뽈찜", "꽃게찜", "김치찜",
            "갈비찜", "마라탕", "오꼬노미야끼", "장어덮밥", "규동", "삼겹살덮밥", "가츠동", "텐동", "스테이크덮밥",
            "쭈꾸미덮밥", "김치볶음밥", "꼬막비빔밥", "양장피", "유산슬", "국수", "카레돈가스", "등심돈가스", "피자", "서브웨이",
            "스테이크", "떡볶이", "로제떡볶이", "알리오올리오", "로제파스타", "크림파스타", "토마토파스타", "후라이드치킨",
            "양념치킨", "간장치킨", "생선구이", "코다리조림", "쪽갈비", "삼겹살", "양념갈비", "한우", "샌드위치", "햄버거"};
        string[] fry_menu = new string[] { "돈코츠라멘", "김치카츠나베", "해물파전", "김치전", "감자전", "후라이드치킨", "양념치킨",
            "간장치킨", "탕수육", "꿔바로우", "카레돈가스", "등심돈가스", "가츠동", "텐동"};

        public MenuRecommend()
        {
            url += "?ServiceKey=" + "pLBMhaAKgs5d%2FhcjmoKmTh9VEIKcq948xlXdnVVv1hwIwjc%2FBQZqHcb1c6SZ2spb%2Br%2B89WN6SjL8gggy2HkwqQ%3D%3D"; // Service Key
            url += "&pageNo=1";
            url += "&numOfRows=1000";
            url += "&dataType=XML";
            url += "&base_date=20230516"; //당일 날짜
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
            //precipitation는 강수형태, humidity는 습도, RN1는 1시간 강수량, tmp는 기온
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
                        if (node1["category"].InnerText.Equals("tmp"))  // 기온
                        {
                            tmp = double.Parse(node1["obsrValue"].InnerText);
                            Console.WriteLine("기온은 {0}", tmp);
                        }

                        if (node1["category"].InnerText.Equals("precipitation"))  // 강수
                        {
                            precipitation = int.Parse(node1["obsrValue"].InnerText);
                            Console.WriteLine("강수형태는 {0}", precipitation);
                        }
                        if (node1["category"].InnerText.Equals("humidity"))  // 습도
                        {
                            precipitation = int.Parse(node1["obsrValue"].InnerText);
                            Console.WriteLine("습도는 {0}", humidity);
                        }
                    }
                }
            }

        }
        ~MenuRecommend()
        {
        }

        public string Recommend()
        {
            Random rnd = new Random();

            //강수형태 없음(0), 기온이 4℃ 이하일 시, hot_menu 랜덤 추출
            //강수형태 없음(0), 기온이 5℃~11℃일 시, hot_menu + else_menu 랜덤 추출
            //강수형태 없음(0), 기온이 12℃~22℃일 시, hot + rain + cold + else 랜덤 추출
            //강수형태 없음(0), 기온이 23℃~27℃, 습도가 70 이하일 시, cold_menu + else_menu 랜덤 추출
            //강수형태 없음(0), 기온이 23℃~27℃, 습도가 70 초과일 시, (cold + else) - fry 랜덤 추출
            //강수형태 없음(0), 기온이 28℃ 이상, 습도가 60 이하일 시, cold_menu 랜덤 추출
            //강수형태 없음(0), 기온이 12℃~22℃, 습도가 60 초과일 시, cold - fry 랜덤 추출
            if (precipitation == 0)
            {
                if (tmp <= 4)
                {
                    menu_result = hot_menu[rnd.Next(hot_menu.Length)];
                }
                else if (tmp >= 5 && tmp <= 11)
                {
                    string[] combined_menu = hot_menu.Concat(else_menu).ToArray();
                    menu_result = GetRandomMenu(combined_menu, rnd);
                }
                else if (tmp >= 12 && tmp <= 22)
                {
                    string[] combined_menu = hot_menu.Concat(else_menu).Concat(cold_menu).Concat(rain_menu).ToArray();
                    menu_result = GetRandomMenu(combined_menu, rnd);
                }
                else if (tmp >= 23 && tmp <= 27)
                {
                    string[] combined_menu = (humidity <= 70) ? cold_menu.Concat(else_menu).ToArray() : cold_menu.Concat(else_menu).Except(fry_menu).ToArray();
                    menu_result = GetRandomMenu(combined_menu, rnd);
                }
                else if (tmp >= 28)
                {
                    string[] combined_menu = (humidity <= 60) ? cold_menu.ToArray() : cold_menu.Except(fry_menu).ToArray();
                    menu_result = GetRandomMenu(combined_menu, rnd);
                }
            }
            //강수형태 비, 소나기, 빗방울 중 하나이고, 기온이 영하 5℃ 미만일 시, hot_menu 랜덤 추출
            //강수형태 비, 소나기, 빗방울 중 하나이고, 기온이 영하 5℃~4℃일 시, hot_menu + rain_menu 랜덤 추출
            //강수형태 비, 소나기, 빗방울 중 하나이고, 기온이 5℃~11℃일 시, hot + rain + else 랜덤 추출
            //강수형태 비, 소나기, 빗방울 중 하나이고, 기온이 12℃~22℃일 시, all 랜덤 추출
            //강수형태 비, 소나기, 빗방울 중 하나이고, 기온이 23℃~27℃일 시, (cold + rain + else) - fry 랜덤 추출
            //강수형태 비, 소나기, 빗방울 중 하나이고, 기온이 28℃ 이상일 시, (cold + rain) - fry 랜덤 추출
            else if (precipitation == 1 || precipitation == 4 || precipitation == 5)
            {
                if (tmp <= 4)
                {
                    string[] combined_menu = (tmp < -5) ? hot_menu.ToArray() : hot_menu.Concat(rain_menu).ToArray();
                    menu_result = GetRandomMenu(combined_menu, rnd);
                }
                else if (tmp >= 5 && tmp <= 11)
                {
                    string[] combined_menu = hot_menu.Concat(else_menu).Concat(rain_menu).ToArray();
                    menu_result = GetRandomMenu(combined_menu, rnd);
                }
                else if (tmp >= 12 && tmp <= 22)
                {
                    string[] combined_menu = hot_menu.Concat(else_menu).Concat(rain_menu).Concat(cold_menu).ToArray();
                    menu_result = GetRandomMenu(combined_menu, rnd);
                }
                else if (tmp >= 23 && tmp <= 27)
                {
                    string[] combined_menu = cold_menu.Concat(else_menu).Concat(rain_menu).Except(fry_menu).ToArray();
                    menu_result = GetRandomMenu(combined_menu, rnd);
                }
                else if (tmp >= 28)
                {
                    string[] combined_menu = cold_menu.Concat(rain_menu).Except(fry_menu).ToArray();
                    menu_result = GetRandomMenu(combined_menu, rnd);
                }
            }
            //강수형태 비·눈, 눈, 빗방울·눈날림, 눈날림 중 하나이고, 기온이 영하 5℃ 이하일 시, hot_menu + else_menu 랜덤 추출
            //강수형태 비·눈, 눈, 빗방울·눈날림, 눈날림 중 하나이고, 기온이 영하 4℃ 이상일 시, hot + rain + else 랜덤 추출
            else if (precipitation == 2 || precipitation == 3 || precipitation == 6 || precipitation == 7)
            {
                string[] combined_menu = (tmp <= -5) ? hot_menu.Concat(else_menu).ToArray() : hot_menu.Concat(rain_menu).Concat(else_menu).ToArray();
                menu_result = GetRandomMenu(combined_menu, rnd);
            }
            Console.WriteLine("결과는 {0}", menu_result);

            int menu_number = Array.IndexOf(all_menu, menu_result) + 1;
            string image_load = menu_number.ToString() + ".jpg";
            Console.WriteLine(image_load);

            return menu_result;
        }
        public string GetRandomMenu(string[] menuArray, Random rnd)
        {
            string selectedMenu = menu_result;
            while (selectedMenu == menu_result)
            {
                int index = rnd.Next(menuArray.Length);
                selectedMenu = menuArray[index];
            }
            return selectedMenu;
        }
        public int findIndex()
        {
            int index = Array.IndexOf(all_menu, menu_result);
            return index;
        }
    }
}
