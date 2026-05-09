using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using 物品包.玩家;

namespace 物品包; 



public class 类型_物品包模组 : Mod {

    public enum 枚举_网络信息类型 : byte {
        饰品包同步,
        护甲包同步
    }

    public override void HandlePacket( BinaryReader 网络流, int 网络玩家ID ) {
        枚举_网络信息类型 信息类型 = ( 枚举_网络信息类型 ) 网络流.ReadByte();

        byte 所属玩家ID = 网络流.ReadByte();
        if ( 所属玩家ID < 0 || 所属玩家ID >= Main.maxPlayers || !Main.player[ 所属玩家ID ].active ) return;

        类型_玩家_物品包 所属玩家;
        switch ( 信息类型 ) {
            case 枚举_网络信息类型.饰品包同步: 所属玩家 = Main.player[ 所属玩家ID ].GetModPlayer<类型_玩家_饰品包>(); break;
            case 枚举_网络信息类型.护甲包同步: 所属玩家 = Main.player[ 所属玩家ID ].GetModPlayer<类型_玩家_护甲包>(); break;
            default: return;
        }

        所属玩家.网络接收( 网络流 );
        if ( Main.netMode == NetmodeID.Server ) 所属玩家.网络发送( -1, 网络玩家ID );
    }

}