﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34011
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataSiftTests {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class AnalysisAPIResponses : global::System.Configuration.ApplicationSettingsBase {
        
        private static AnalysisAPIResponses defaultInstance = ((AnalysisAPIResponses)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new AnalysisAPIResponses())));
        
        public static AnalysisAPIResponses Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"[
   {
      ""id"":""bb72ad2c2c9d27c14ab023303e58ec93"",
      ""user_id"":28745,
      ""volume"":0,
      ""start"":1417189947,
      ""end"":1417189990,
      ""status"":""stopped"",
      ""origin"":""api"",
      ""name"":""newyork"",
      ""reached_capacity"":true,
      ""remaining_capacity"":1000000,
      ""remaining_total_capacity"":0
   }
]")]
        public string Get {
            get {
                return ((string)(this["Get"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("{\"created_at\":\"1417191702\",\"dpu\":\"0.1\"}")]
        public string Validate {
            get {
                return ((string)(this["Validate"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("{\"hash\":\"58eb8c4b74257406547ab1ed3be346a8\",\"created_at\":\"1417428264\",\"dpu\":\"0.1\"}" +
            "")]
        public string Compile {
            get {
                return ((string)(this["Compile"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"{
   ""volume"":0,
   ""start"":1417435530,
   ""end"":1417435716,
   ""status"":""stopped"",
   ""name"":""Example recording"",
   ""reached_capacity"":false,
   ""remaining_capacity"":1000000,
   ""remaining_total_capacity"":100000,
   ""hash"":""58eb8c4b74257406547ab1ed3be346a8""
}")]
        public string GetOne {
            get {
                return ((string)(this["GetOne"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("{\"interactions\":0,\"unique_authors\":0,\"analysis\":{\"analysis_type\":\"freqDist\",\"para" +
            "meters\":{\"threshold\":5,\"target\":\"fb.author.age\"},\"results\":[],\"redacted\":false}}" +
            "")]
        public string Analyze {
            get {
                return ((string)(this["Analyze"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("[\"tag.one\",\"tag.two\",\"tag.three\"]")]
        public string Tags {
            get {
                return ((string)(this["Tags"]));
            }
        }
    }
}
