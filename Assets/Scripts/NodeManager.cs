/* ========================================================
 *	作 者：ZhangShouYang 
 *	创建时间：2018/05/15 16:57:39
 *	版 本：v 1.0
 *	描 述：游戏中的音乐块抽象为节点 Node,有关node的类表示的便是音乐键。
 *	    管理器作用相当于Producer,负责生成对应的音乐块，具体的生成逻辑都在这里完成，包括根据音乐节奏，生成算法等。
 *	    并赋予对应的滑块相应的属性，比如形状、时间片、分数、分数的计算公式等
 *	注：不在管理器中添加node列表，简化管理器功能，将对node的控制操作分散在轨道和node自身，管理器只承担Producer功能
 *	    该类全局只会实例化一次，因此可以设计为单例模式
* ========================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PT2
{
    public class NodeManager: XSingleton<NodeManager>
    {
        // 滑块节点的类型
        public enum eNodeType{
            Normal = 0,     // 普通
        }

        // 缓存池
        private Dictionary<int, GameObject> _prefabList = null;
        private Dictionary<int, List<ModelNode>> _nodePool = null;
        public int m_CacheDefualtNum = 10;     // 每类节点资源默认的初始数量

        // Init Operate初始化操作
        public bool Init()
        {
            // TODO: 初始化一些配置信息,下面的代码需要进一步整理和优化，需要等到资源模块完成后重新整理为异步操作
            _prefabList = new Dictionary<int, GameObject>();
            _nodePool = new Dictionary<int, List<ModelNode>>();

            int len = Game.Instance.blockPrefabs.Length;
            if(len > 0)
            {
                for(int i = 0; i<len; ++ i)
                {
                    GameObject obj = Game.Instance.blockPrefabs[i];
                    _prefabList.Add(i, obj);

                    CrateModelNodePool(i, obj, m_CacheDefualtNum);
                }
            }           

            return true;
        }

        // 创建一类节点缓存
        private void CrateModelNodePool(int mType, GameObject prefabe, int num)
        {
            if(_nodePool.ContainsKey(mType))
            {
                Debug.LogWarning("Already contain this type node cache "+mType);
                return;
            }
            if(prefabe == null)
            {
                Debug.LogError("Created failed ,node resource is a null value");
                return;
            }
            List<ModelNode> list = new List<ModelNode>();
            for(int i = 0;i< num; ++ i)
            {               
                eNodeType eType = (eNodeType)mType;

                ModelNode node = CreateEmptyNode(eType, prefabe);
               
                list.Add(node);
            }
            _nodePool.Add(mType, list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eType"></param>
        /// <param name="prefab">源资源prefab</param>
        /// <returns></returns>
        private ModelNode CreateEmptyNode(eNodeType eType,UnityEngine.Object prefab)
        {
            if (prefab == null)
                return null;

            ModelNode node = null;
            GameObject go = GameObject.Instantiate(prefab) as GameObject;

            switch (eType)
            {
                case eNodeType.Normal:
                    node = new NormalNode(go);
                    break;
                default:
                    node = new NormalNode(go);
                    break;
            }
            node.Init();

            return node;
        }

        private ModelNode PickAvaliableNode(eNodeType eType)
        {
            int mType = (int)eType;
            List<ModelNode> list = null;
            ModelNode node = null;

            if(!_nodePool.TryGetValue(mType,out list))
            {
                GameObject prefabe = _prefabList[mType];
                if (prefabe == null)
                {
                    Debug.LogError("Not contain this type prefab "+mType);
                    return null;
                }
                CrateModelNodePool(mType, prefabe, m_CacheDefualtNum);
            }

            int count = list.Count;
            if(count <= 0)
            {
                Object prefabe = _prefabList[mType];
                if (prefabe == null)
                {
                    Debug.LogError("Not contain this type prefab@@@@@@ " + mType);
                    return null;
                }

                node = CreateEmptyNode(eType, prefabe);

                return node;
            }

            node = list[0];
            list.RemoveAt(0);

            return null;
        }

        #region Create Node Operates
        
        /// <summary>
        /// 根据时间片长短创建
        /// </summary>
        /// <param name="dataSet">数据集</param>
        /// <returns></returns>
        public ModelNode ProduceModelNode( int mType, Hashtable dataSet)
        {
            // Creat modelnode,生成节点
            ModelNode node = PickAvaliableNode((eNodeType)mType);
            if (node == null)
                return null;
            
            // ToDo: Set node attriInfo,将数据集传给
                   

            // Put on track ,将节点放入轨道



            return node;
        }

        #endregion

        // 更新不由引擎驱动，而是由Game驱动
        public void Update()
        {

        }
    }
}

