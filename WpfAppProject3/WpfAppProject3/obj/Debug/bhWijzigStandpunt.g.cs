#pragma checksum "..\..\bhWijzigStandpunt.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "05A7334A5C3ECBAB12C6F420AEA987F3BA9675A56F95414F222B293957DAD74B"
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
    /// bhWijzigStandpunt
    /// </summary>
    public partial class bhWijzigStandpunt : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\bhWijzigStandpunt.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbxHeader;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\bhWijzigStandpunt.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbxID;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\bhWijzigStandpunt.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbxStandpunt;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\bhWijzigStandpunt.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbxThema;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\bhWijzigStandpunt.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btUpdate;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfAppProject3;component/bhwijzigstandpunt.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\bhWijzigStandpunt.xaml"
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
            this.tbxHeader = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.tbxID = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.tbxStandpunt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.cbxThema = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.btUpdate = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\bhWijzigStandpunt.xaml"
            this.btUpdate.Click += new System.Windows.RoutedEventHandler(this.btUpdate_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

