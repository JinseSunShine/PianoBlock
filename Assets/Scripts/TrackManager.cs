/* ========================================================
 *	作 者：ZhangShouYang 
 *	创建时间：2018/05/15 18:54:53
 *	版 本：v 1.0
 *	描 述：全局只有一份实例，可以设计为单例模式； 轨道管理模块，功能如下：
 *	   1.轨道管理，负责分配具体的轨道(包括轨道的数量，轨道添加或减少的逻辑控制)
 *	   2.以及轨道上的各种效果展示，如某条轨道高亮，整体运动、轨道弯曲等
 *	   3.给予轨道上的滑块某种运动约束(轨道运动算法)，支持轨道上的滑块产生符合轨道要求的运动轨迹
 *	   4.提供每根轨道上的当前滑块的占用时间区域，避免不同时刻的滑块出现重叠现象
 *	   5.轨道根据自身的设置和算法将当前时刻生成的滑块放到合适的轨道上，避免滑块重叠或是滑块边界超出轨道范围比如调整滑块的旋转等
* ========================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PT2
{
    public class TrackManager: XSingleton<TrackManager>
    {
        private int m_TrackNum = 4;             // 轨道数量

        private List<IModelNode> _prepareList;   // 预备队列，等待加入轨道的节点列表
        private List<IModelNode> _operateList;   // 操控队列，进入并受轨道控制的节点列表

        public bool Init()
        {
            if(_prepareList == null)            
                _prepareList = new List<IModelNode>();
            if(_operateList == null)
                _operateList = new List<IModelNode>();

            return true;
        }

        public void ChangeTrackNum(int value)
        {
            m_TrackNum = value;
           
            // 修改轨道的显示
        }

        // 压入节点数据
        public bool PushValue(IModelNode node)
        {
            if (!node.IsAvailable())
            {
                return false;
            }

            _prepareList.Add(node);

            return true;
        }


        // 由Game驱动
        public void Update()
        {
            UpdateOperateList();

            UpdatePrepareList();            
        }

        // 更新操作队列中的节点，控制其按照轨道规则运动
        private void UpdateOperateList()
        {

        }

        // 更新预备队列中的数据，当有节点满足进入轨道条件则安排其进入轨道
        private void UpdatePrepareList()
        {

        }

        
    }
}

