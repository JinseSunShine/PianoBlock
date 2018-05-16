// ========================================================
	// 作 者：ZhangShouYang 
	// 创建时间：2018/05/15 15:50:33
	// 版 本：v 1.0
	// 描 述：游戏的逻辑控制入口、游戏主要逻辑，控制游戏主循环

// ========================================================
using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace PT2
{
    public class Game : MonoBehaviour
    {
        // 临时外部引用，以后需要移到配置中
        public GameObject[] blockPrefabs;  // 滑块的prefab


        ////////////////////////////////////////////////

        static protected Game _instance = null;

        protected Camera _pMainCamera = null;

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
                    
        }

        void Start()
        {
            if(_instance == null)
            {
                _instance = this;
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
                Application.runInBackground = true;
            }
            else
            {
                GameObject.Destroy(this);
                return;
            }

            // TODO: Prepare game start environment.
            NodeManager.instance.Init();
            TrackManager.instance.Init();           

        }

        public static Game Instance
        {
            get { return _instance; }
        }

        // Update is called once per frame
        void Update()
        {
            // 测试操作
            if (Input.GetKeyDown(KeyCode.Space))
            {
                int timeValue = UnityEngine.Random.Range(1, 5);
                NodeManager.instance.ProduceModelNode(0, null);
            }
        }

        // LateUpdate is called after all update
        void LateUpdate()
        {
            float fTime = Time.time;

            NodeManager.instance.Update();
            TrackManager.instance.Update();
        }

       
    }
}

