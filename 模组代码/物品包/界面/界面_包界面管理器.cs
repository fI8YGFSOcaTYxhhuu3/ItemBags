using System;
using System.Collections.Generic;
using Terraria.UI;
using 物品包.Items;

namespace 物品包.界面;



public class 类型_包界面管理器 : UIState {

    private readonly Dictionary<Guid, 类型_包窗口> 窗口映射 = [];

    public void 切换窗口( 类型_物品包 包 ) {
        if ( 窗口映射.ContainsKey( 包.ID ) ) 关闭窗口( 包.ID );
        else {
            var 窗口 = new 类型_包窗口( 包 );
            窗口映射.Add( 包.ID, 窗口 );
            Append( 窗口 );
        }
    }

    public void 关闭窗口( Guid ID ) { 窗口映射[ ID ].Remove(); 窗口映射.Remove( ID ); }

    public void 关闭全部() {
        foreach ( var 窗口 in 窗口映射.Values ) 窗口.Remove();
        窗口映射.Clear();
    }

}