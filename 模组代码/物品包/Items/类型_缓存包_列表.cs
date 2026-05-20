using System.Collections.Generic;

namespace 物品包.Items;



// 特征重写函数
public partial interface 接口_缓存包_列表<类型_缓存数据> : 接口_缓存包<List<类型_缓存数据>> {
    void 接口_缓存包.清空缓存() => 缓存数据.Clear();
    List<类型_缓存数据> 接口_缓存包<List<类型_缓存数据>>.初始缓存数据() => [];
}