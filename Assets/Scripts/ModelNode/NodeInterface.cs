/* ========================================================
 *	作 者：ZhangShouYang 
 *	创建时间：2018/05/15 17:20:57
 *	版 本：v 1.0
 *	描 述：钢琴键节点接口类，提供节点所需的对外接口
* ========================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PT2
{
    public interface IModelNode
    {

        bool IsAvailable();
        void OnTriggerEnter();
        void OnTriggerExit();
        void OnTriggleStay();
        bool IsNodePressed();

    }



    /* ========================================================
    *	描 述：抽象类，所有的差异性节点都由此派生
    * ========================================================*/
    public abstract class ModelNode:IModelNode
    {
        protected GameObject _gameObject;
        protected Transform _trans;
        protected DataModel modelData = new DataModel();
      
        public ModelNode(GameObject go)
        {
            if(go != null)
            {
                _gameObject = go;
                _trans = go.transform;

                modelData.ori_scale = _trans.localScale;
                modelData.ori_rotation = _trans.localRotation;
            }
        }

        public virtual void Init()
        {
            // 数据初始化操作
        }
        
        public virtual bool IsAvailable()
        {
            return true;
        }
        public virtual void OnTriggerEnter()
        { }
        public virtual void OnTriggerExit()
        { }
        public virtual void OnTriggleStay()
        { }
        public virtual bool IsNodePressed()
        {
            return false;
        }

        #region 模型的数据部分
        [System.Serializable]
        public class DataModel
        {
            // 原始数据
            public Vector3 ori_scale = Vector3.one;
            public Quaternion ori_rotation = Quaternion.identity;

            // 修改后的数据
            public Vector3 scale = Vector3.one;
            public Quaternion rotation = Quaternion.identity;
            
        }

        #endregion
    }
}
