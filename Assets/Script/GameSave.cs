using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Text;
using System;
using System.Linq;

namespace GameSave {
    public class Game : MonoBehaviour {
        private static string userName = Environment.UserName;
        private static string folderPath = @"C:\Users\" + userName + @"\Documents\AngryBird";
        private static string filePath = @"C:\Users\" + userName + @"\Documents\AngryBird\save.json";
        // 使用json存储
        private static JObject data;

        public static void createFolder() {
            if (!Directory.Exists(folderPath)) {
                Directory.CreateDirectory(folderPath);
                Debug.Log("create folder");
            }
        } // 创建文件夹

        public static void createFile() {
            createFolder();
            if (!File.Exists(filePath)) {
                File.Create(filePath).Dispose();
                Debug.Log("create file");
            }
        } // 创建文件

        public static void writeInitialConfig() {
            File.WriteAllText(filePath, @"{
                ""stars"" : 0,
                ""map"" : [
                    { ""stars"" : 0, ""level"" : [0] },
                    { ""stars"" : 0, ""level"" : [0] },
                    { ""stars"" : 0, ""level"" : [0] },
                    { ""stars"" : 0, ""level"" : [0] }
                ]
            }");
            Debug.Log("write initial config");
        } // 写入初始配置文件

        public static string readFile() {
            return File.ReadAllText(filePath, Encoding.Default);
        } // 读取文件

        public static JObject levelData(int map, int level, int star) {
            string str = @"{ ""map"" : "" "", ""level"" : "" "", ""star"" : "" "" }";
            JObject jobject = JObject.Parse(str);
            jobject["map"] = map;
            jobject["level"] = level;
            jobject["star"] = star;
            //Debug.Log(Convert.ToString(jobject));
            return jobject;
        } // 获取关卡数据

        public static void newLevel(int map) {
            // 扩展用于储存关卡星星的数组
            // 如果数组的最后一个数字大于0, 说明
            // 最后一关已经通过, 可以开启下一关
            JToken star = data["map"][map]["level"][data["map"][0]["level"].Count() - 1];
            if ((int)star > 0) {
                star.AddAfterSelf(0);
            }
        } // 开启新的一关

        public static void totalStars() {
            int starCount = 0;
            for (int i = 0; i < data["map"].Count(); i++) {
                mapTotalStars(i);
                starCount += (int)data["map"][i]["stars"];
            }
            data["stars"] = starCount;
            //Debug.Log("total");
        } // 计算所有星星总数量

        public static void levelStar(JObject jobject) {
            /*
             总地图:
                第几张地图:
                    记录关卡星数:
                        第几关 = 星数 
             */
             // 如果现在的星数小于获得的星数就覆盖, 否则跳过
             if ((int)data["map"][(int)jobject["map"]]["level"][(int)jobject["level"]] < (int)jobject["star"]) {
                data["map"][(int)jobject["map"]]["level"][(int)jobject["level"]] = jobject["star"];
            }
            
        } // 修改关卡星数

        public static void mapTotalStars(int index) {
            int starCount = 0;
            for (int i = 0; i < data["map"][index]["level"].Count(); i++) {
                starCount += (int)data["map"][index]["level"][i];
            }
            data["map"][index]["stars"] = starCount;
            //Debug.Log("mapTotalStars");
        } // 计算每张地图的星数

        public static void changeFile(int map, int level, int star) {
            map--;
            level--;
            try {
                // 获取json文件内容并解析
                data = JObject.Parse(readFile());
                levelStar(levelData(map, level, star));
                totalStars();
                newLevel(map);
                //将内容写进json文件中
                //Debug.Log(Convert.ToString(data));
                File.WriteAllText(filePath, Convert.ToString(data), Encoding.UTF8);
            } catch (Exception) { // 如果文件内没有json就在文件内写入初始配置内容
                Initialize();
            }
        } // 修改json内容

        public static void Initialize() {
            try {
                // 获取json文件内容并解析
                data = JObject.Parse(readFile());
            } catch (Exception) { // 如果文件内没有json就在文件内写入初始配置内容
                writeInitialConfig();
            }
            // 初始化后把json的值传给data, 方便之后的修改
            data = JObject.Parse(readFile());
        } // 进行初始化

        public static int GetStarCount() {
            return (int)data["stars"];
        } // 返回地图总星数

        public static int GetMapStar(int map) {
            return (int)data["map"][map - 1]["stars"];
        } // 返回指定地图的星数

        public static int GetLevelStar(int map, int level) {
            return (int)data["map"][map-1]["level"][level-1];
        } // 返回指定关卡的星数

        public static int GetLevel(int map) {
            return data["map"][map - 1]["level"].Count();
        } // 返回指定地图开启的关卡数

        public static void Action() {
            createFile();
            Initialize();
        }

    }
}

