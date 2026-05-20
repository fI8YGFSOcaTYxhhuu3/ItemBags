using System.IO;
using Terraria.ModLoader.IO;
using 物品包.玩家;

namespace 物品包.Items;



// 特征成员
public partial interface 接口_缓存包_可切换 : 接口_缓存包 {
    bool 启用状态 { get; set; }

    void 切换启用状态() => 切换启用状态_缓存包_可切换();
    protected void 切换启用状态_缓存包_可切换() {
        启用状态 = !启用状态;
        脏标记 = true;
        if ( 玩家 is 类型_玩家_缓存包 缓存玩家 ) 缓存玩家.脏标记_缓存包 = true;
        Terraria.Audio.SoundEngine.PlaySound( Terraria.ID.SoundID.MenuTick );
    }
}

// 特征重写函数
public partial interface 接口_缓存包_可切换<类型_缓存数据> : 接口_缓存包_可切换, 接口_缓存包<类型_缓存数据> {
    void 接口_缓存包.更新缓存() {
        if ( !脏标记 ) return;
        脏标记 = false;
        if ( 缓存数据 == null ) 缓存数据 = 初始缓存数据();
        else 清空缓存();
        if ( 启用状态 ) 建立缓存();
    }
    void 接口_物品包.SaveData( TagCompound 存档标签 ) { SaveData_物品包( 存档标签 ); 存档标签[ "启用状态" ] = 启用状态; }
    void 接口_物品包.LoadData( TagCompound 存档标签 ) { LoadData_缓存包( 存档标签 ); 启用状态 = 存档标签.GetBool( "启用状态" ); }
    void 接口_物品包.NetSend( BinaryWriter 网络流 ) { NetSend_物品包( 网络流 ); 网络流.Write( 启用状态 ); }
    void 接口_物品包.NetReceive( BinaryReader 网络流 ) { NetReceive_缓存包( 网络流 ); 启用状态 = 网络流.ReadBoolean(); }
}