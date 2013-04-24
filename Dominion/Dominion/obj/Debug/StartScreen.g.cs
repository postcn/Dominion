﻿#pragma checksum "..\..\StartScreen.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5B8B67DDF4F3BB2497CAE13B1BE11E3E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18034
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


namespace Dominion {
    
    
    /// <summary>
    /// StartScreen
    /// </summary>
    public partial class StartScreen : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\StartScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem English;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\StartScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Something_Lame;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\StartScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ConfirmButton;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\StartScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PlayNumText;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\StartScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameBox1;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\StartScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameBox2;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\StartScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameBox3;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\StartScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameBox4;
        
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
            System.Uri resourceLocater = new System.Uri("/Dominion;component/startscreen.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\StartScreen.xaml"
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
            this.English = ((System.Windows.Controls.MenuItem)(target));
            
            #line 13 "..\..\StartScreen.xaml"
            this.English.Click += new System.Windows.RoutedEventHandler(this.ChangeLanguage);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Something_Lame = ((System.Windows.Controls.MenuItem)(target));
            
            #line 14 "..\..\StartScreen.xaml"
            this.Something_Lame.Click += new System.Windows.RoutedEventHandler(this.ChangeLanguage);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ConfirmButton = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\StartScreen.xaml"
            this.ConfirmButton.Click += new System.Windows.RoutedEventHandler(this.ConfirmNames);
            
            #line default
            #line hidden
            return;
            case 4:
            this.PlayNumText = ((System.Windows.Controls.TextBox)(target));
            
            #line 18 "..\..\StartScreen.xaml"
            this.PlayNumText.GotFocus += new System.Windows.RoutedEventHandler(this.ChangeNumber);
            
            #line default
            #line hidden
            return;
            case 5:
            this.NameBox1 = ((System.Windows.Controls.TextBox)(target));
            
            #line 19 "..\..\StartScreen.xaml"
            this.NameBox1.GotFocus += new System.Windows.RoutedEventHandler(this.NameBoxFocus);
            
            #line default
            #line hidden
            return;
            case 6:
            this.NameBox2 = ((System.Windows.Controls.TextBox)(target));
            
            #line 20 "..\..\StartScreen.xaml"
            this.NameBox2.GotFocus += new System.Windows.RoutedEventHandler(this.NameBoxFocus);
            
            #line default
            #line hidden
            return;
            case 7:
            this.NameBox3 = ((System.Windows.Controls.TextBox)(target));
            
            #line 21 "..\..\StartScreen.xaml"
            this.NameBox3.GotFocus += new System.Windows.RoutedEventHandler(this.NameBoxFocus);
            
            #line default
            #line hidden
            return;
            case 8:
            this.NameBox4 = ((System.Windows.Controls.TextBox)(target));
            
            #line 22 "..\..\StartScreen.xaml"
            this.NameBox4.GotFocus += new System.Windows.RoutedEventHandler(this.NameBoxFocus);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

