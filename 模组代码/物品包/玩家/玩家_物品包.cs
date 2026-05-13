using System.IO;
using Terraria.ModLoader;

namespace 物品包.玩家;



public class 类型_玩家_物品包 : ModPlayer {
    public override void SyncPlayer( int toWho, int fromWho, bool newPlayer ) { 网络发送( toWho, fromWho ); }
    public virtual void 网络发送( int 接收玩家ID, int 发送玩家ID ) { }
    public virtual void 网络接收( BinaryReader 网络流 ) { }
}