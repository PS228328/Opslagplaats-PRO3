﻿#pragma checksum "..\..\bhVerkiezingen.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "70EE90A2A1D805734FAB4EE1453FA8C8F6D8B3138E4497878FAC4F25D43D3414"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using WpfAppProject3;


namespace WpfAppProject3 {
    
    
    /// <summary>
    /// bhVerkiezingen
    /// </summary>
    public partial class bhVerkiezingen : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\bhVerkiezingen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox databaseitems;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\bhVerkiezingen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WrapPanel wpAdd;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\bhVerkiezingen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbSoort;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\bhVerkiezingen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dpDatum;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\bhVerkiezingen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btVerzenden;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\bhVerkiezingen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btAdd;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\bhVerkiezingen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btGoBack;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfAppProject3;component/bhverkiezingen.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\bhVerkiezingen.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.databaseitems = ((System.Windows.Controls.ListBox)(target));
            return;
            case 2:
            this.wpAdd = ((System.Windows.Controls.WrapPanel)(target));
            return;
            case 3:
            this.cbSoort = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.dpDatum = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 5:
            this.btVerzenden = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\bhVerkiezingen.xaml"
            this.btVerzenden.Click += new System.Windows.RoutedEventHandler(this.btVerzenden_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btAdd = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\bhVerkiezingen.xaml"
            this.btAdd.Click += new System.Windows.RoutedEventHandler(this.btAdd_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btGoBack = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\bhVerkiezingen.xaml"
            this.btGoBack.Click += new System.Windows.RoutedEventHandler(this.btGoBack_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
