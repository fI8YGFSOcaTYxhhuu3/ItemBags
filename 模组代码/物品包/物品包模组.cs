using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using 物品包.玩家;
using 物品包.配置;

namespace 物品包; 



public class 类型_物品包模组 : Mod {

    public enum 枚举_同步操作类型 : byte {
        玩家同步,
        配置同步,
    }

    public enum 枚举_同步对象类型 : byte {
        饰品包,
        护甲包,
    }

    public override void HandlePacket( BinaryReader 网络流, int 发信玩家ID ) {
        枚举_同步操作类型 操作类型 = ( 枚举_同步操作类型 ) 网络流.ReadByte();
        枚举_同步对象类型 对象类型 = ( 枚举_同步对象类型 ) 网络流.ReadByte();

        byte 所属玩家ID = 网络流.ReadByte();
        if ( 所属玩家ID < 0 || 所属玩家ID >= Main.maxPlayers || !Main.player[ 所属玩家ID ].active ) return;

        switch ( 操作类型 ) {
            case 枚举_同步操作类型.玩家同步: HandlePacket_玩家同步( 网络流, 对象类型, 所属玩家ID, 发信玩家ID ); return;
            case 枚举_同步操作类型.配置同步: HandlePacket_配置同步( 网络流, 对象类型, 所属玩家ID, 发信玩家ID ); return;
        }
    }

    private static void HandlePacket_玩家同步( BinaryReader 网络流, 枚举_同步对象类型 对象类型, byte 所属玩家ID, int 网络玩家ID ) {
        var 玩家 = Main.player[ 所属玩家ID ]; 类型_玩家_物品包 所属玩家;
        switch ( 对象类型 ) {
            case 枚举_同步对象类型.饰品包: 所属玩家 = 玩家.GetModPlayer<类型_玩家_饰品包>(); break;
            case 枚举_同步对象类型.护甲包: 所属玩家 = 玩家.GetModPlayer<类型_玩家_护甲包>(); break;
            default: return;
        }

        所属玩家.网络接收( 网络流 );
        if ( Main.netMode == NetmodeID.Server ) 所属玩家.网络发送( -1, 网络玩家ID );
    }

    private static void HandlePacket_配置同步( BinaryReader 网络流, 枚举_同步对象类型 对象类型, byte 所属玩家ID, int 网络玩家ID ) {
        var 玩家 = Main.player[ 所属玩家ID ]; 类型_配置_物品包 配置对象;
        switch ( 对象类型 ) {
            case 枚举_同步对象类型.饰品包: 配置对象 = 玩家.GetModPlayer<类型_玩家_饰品包>().配置; break;
            case 枚举_同步对象类型.护甲包: 配置对象 = 玩家.GetModPlayer<类型_玩家_护甲包>().配置; break;
            default : return;
        }

        配置对象.网络接收( 网络流, 所属玩家ID );
        if ( Main.netMode == NetmodeID.Server ) 配置对象.网络发送( -1, 网络玩家ID );
    }

}