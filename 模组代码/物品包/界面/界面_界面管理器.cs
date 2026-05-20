using System;
using System.Collections.Generic;
using Terraria.UI;
using 物品包.Items;

namespace 物品包.界面;



// 通用
public partial class 类型_界面管理器 : UIState {
    public void 关闭全部() {
        foreach ( var 窗口 in 窗口映射.Values ) 窗口.Remove();
        窗口映射.Clear();
        关闭配置窗口();
    }
}

// 物品包窗口
public partial class 类型_界面管理器 {
    private readonly Dictionary<Guid, 类型_窗口_物品包> 窗口映射 = [];

    public void 切换窗口( 接口_物品包 包 ) {
        if ( 窗口映射.ContainsKey( 包.ID ) ) 关闭窗口( 包.ID );
        else {
            var 窗口 = new 类型_窗口_物品包( 包 );
            窗口映射.Add( 包.ID, 窗口 );
            Append( 窗口 );
        }
    }
    public void 关闭窗口( Guid ID ) { 窗口映射[ ID ].Remove(); 窗口映射.Remove( ID ); if ( 配置窗口?.包.ID == ID ) 关闭配置窗口(); }
}

// 配置窗口
public partial class 类型_界面管理器 {
    public 类型_窗口_物品包配置 配置窗口;

    public void 切换配置窗口( 接口_物品包 包 ) {
        if ( 配置窗口 == null ) { 创建配置窗口( 包 ); return; }
        if ( 配置窗口.包.ID == 包.ID ) { 关闭配置窗口(); return; }
        关闭配置窗口(); 创建配置窗口( 包 );
    }
    public void 创建配置窗口( 接口_物品包 包 ) { 配置窗口 = new( 包 ); Append( 配置窗口 ); }
    public void 关闭配置窗口() { 配置窗口?.Remove(); 配置窗口 = null; }
}