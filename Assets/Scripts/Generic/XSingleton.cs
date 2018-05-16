/* ========================================================
 *	作 者：ZhangShouYang 
 *	创建时间：2018/05/16 10:10:53
 *	版 本：v 1.0
 *	描 述：单例模板
* ========================================================*/

using System.Collections;
using UnityEngine;

namespace PT2
{
    public abstract class XSingleton<T> where T:class, new()
    {

        public static T instance
        {
            get { return XSingletonCreator.instance; }
        }

        class XSingletonCreator
        {
            internal static readonly T instance = new T();
        }
    }

    
}

