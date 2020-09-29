(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-6136713b"],{"0d87":function(e,t,n){"use strict";var r=n("56d7");t["a"]={mounted:function(){r["EventBus"].$emit("setPageTitle",this.pageTitle)},watch:{pageTitle:function(e){r["EventBus"].$emit("setPageTitle",e)}}}},"169b":function(e,t,n){"use strict";n("99af"),n("a15b"),n("96cf");var r=n("1da1"),a=n("3a78");t["a"]={rootURL:"/pallet",list:function(){var e=this.rootURL;return a["a"].get(e)},getById:function(e){var t="".concat(this.rootURL,"/").concat(e);return a["a"].get(t)},create:function(e){var t="".concat(this.rootURL);return a["a"].post(t,e)},update:function(e,t){var n="".concat(this.rootURL,"/").concat(e);return a["a"].put(n,t)},bulkDelete:function(e){var t=this;return Object(r["a"])(regeneratorRuntime.mark((function n(){var r,c,o;return regeneratorRuntime.wrap((function(n){while(1)switch(n.prev=n.next){case 0:if(0!=e.length){n.next=2;break}return n.abrupt("return");case 2:return r=[],c=e.join(","),o="".concat(t.rootURL,"/BulkDelete/").concat(c),n.prev=5,n.next=8,a["a"].delete(o).then((function(e){return console.log(e)}));case 8:n.next=15;break;case 10:throw n.prev=10,n.t0=n["catch"](5),console.error(o,n.t0),r.push(n.t0),r;case 15:case"end":return n.stop()}}),n,null,[[5,10]])})))()},delete:function(e){var t=this;return Object(r["a"])(regeneratorRuntime.mark((function n(){var r,c,o;return regeneratorRuntime.wrap((function(n){while(1)switch(n.prev=n.next){case 0:if(null!=e){n.next=2;break}throw"Please select a record to Delete";case 2:return r=[],c="",n.prev=4,c="".concat(t.rootURL,"/").concat(e),n.next=8,a["a"].delete(c).then((function(e){return console.log(e)}));case 8:n.next=15;break;case 10:throw n.prev=10,n.t0=n["catch"](4),console.error(c,n.t0),r.push(n.t0),r;case 15:return o={},o=0==r.length?{status:"200",statusText:'Deleting of "'.concat(e.BinLocDesc,'" Successful')}:{status:"404",statusText:"Failed to delete some of the selected records",errors:r},n.abrupt("return",o);case 18:case"end":return n.stop()}}),n,null,[[4,10]])})))()}}},a1c2:function(e,t,n){"use strict";n.r(t);var r=function(){var e=this,t=e.$createElement,n=e._self._c||t;return n("div",{staticClass:"pa-10"},[n("v-sheet",{staticClass:"elevation-1"},[n("v-data-table",{attrs:{"item-key":"PalletId",headers:e.tblHeaders,items:e.items,search:e.search,"show-select":""},scopedSlots:e._u([{key:"top",fn:function(){return[n("v-toolbar",{attrs:{flat:""}},[n("ButtonDelete",{on:{click:function(t){return e.remove()}}}),n("v-spacer"),n("ButtonAdd",{attrs:{to:{name:"PalletCreate"}}}),n("v-text-field",{staticStyle:{"max-width":"320px"},attrs:{label:"Search by Name",color:"grey",outlined:"",dense:"","hide-details":"","prepend-icon":"mdi-search","show-select":!0},model:{value:e.search,callback:function(t){e.search=t},expression:"search"}})],1)]},proxy:!0},{key:"item.PalletId",fn:function(t){var r=t.item;return[n("v-btn",{staticClass:"elevation-1",attrs:{fab:"","x-small":"",to:{name:"PalletUpdate",params:{id:r.PalletId}}}},[n("v-icon",{attrs:{color:"success"}},[e._v("mdi-pencil")])],1)]}}]),model:{value:e.checkedRows,callback:function(t){e.checkedRows=t},expression:"checkedRows"}})],1),n("router-view"),n("MessageBox",{ref:"messagebox"})],1)},a=[],c=(n("99af"),n("d81d"),n("96cf"),n("1da1")),o=n("0d87"),s=n("cca5"),i=n("169b"),l={name:"Pallets",mixins:[o["a"],s["a"]],data:function(){return{pageTitle:"Pallets",checkedRows:[],search:"",items:[],tblHeaders:[{text:"Pallet Number",value:"PalletNum",sortable:!0,align:"left"},{text:"Weight (Kg)",value:"UpdatedWt",sortable:!0,align:"left"},{text:"action",value:"PalletId",align:"right"}]}},watch:{},created:function(){var e=this;return Object(c["a"])(regeneratorRuntime.mark((function t(){return regeneratorRuntime.wrap((function(t){while(1)switch(t.prev=t.next){case 0:return t.next=2,e.init();case 2:case"end":return t.stop()}}),t)})))()},methods:{init:function(){var e=this;return Object(c["a"])(regeneratorRuntime.mark((function t(){var n;return regeneratorRuntime.wrap((function(t){while(1)switch(t.prev=t.next){case 0:return t.prev=0,t.next=3,i["a"].list();case 3:n=t.sent,e.items=n.data,e.checkedRows=[],t.next=11;break;case 8:t.prev=8,t.t0=t["catch"](0),console.log(t.t0);case 11:case"end":return t.stop()}}),t,null,[[0,8]])})))()},remove:function(){var e=this;return Object(c["a"])(regeneratorRuntime.mark((function t(){var n,r;return regeneratorRuntime.wrap((function(t){while(1)switch(t.prev=t.next){case 0:if(0!=e.checkedRows.length){t.next=2;break}return t.abrupt("return");case 2:n={title:"Delete Confirmation",content:"Do you want to delete selected items(".concat(e.checkedRows.length,")"),buttons:"deleteCancel"},r={visible:!0},e.$refs.messagebox.open(n,Object(c["a"])(regeneratorRuntime.mark((function t(){var n;return regeneratorRuntime.wrap((function(t){while(1)switch(t.prev=t.next){case 0:return t.prev=0,n=e.checkedRows.map((function(e){return e.PalletId})),t.next=4,i["a"].bulkDelete(n);case 4:r.type="success",r.content="".concat(n.length," ").concat(1==n.length?"record":"records"," deleted successfully"),e.init(),t.next=14;break;case 9:t.prev=9,t.t0=t["catch"](0),r.type="error",e.internalErrorMsg=t.t0,r.content=t.t0;case 14:return t.prev=14,e.showNotification(r),t.finish(14);case 17:case"end":return t.stop()}}),t,null,[[0,9,14,17]])}))));case 5:case"end":return t.stop()}}),t)})))()}},beforeRouteUpdate:function(e,t,n){1==!t.meta.cancelled&&this.init(),n()}},u=l,d=n("2877"),h=n("6544"),f=n.n(h),p=n("8336"),v=n("8fea"),m=n("132d"),b=n("8dd9"),w=n("2fa4"),g=n("8654"),x=n("71d9"),k=Object(d["a"])(u,r,a,!1,null,null,null);t["default"]=k.exports;f()(k,{VBtn:p["a"],VDataTable:v["a"],VIcon:m["a"],VSheet:b["a"],VSpacer:w["a"],VTextField:g["a"],VToolbar:x["a"]})},cca5:function(e,t,n){"use strict";var r=n("5530"),a=n("2f62");t["a"]={methods:Object(r["a"])({},Object(a["b"])("notification",{showNotification:"show"}))}}}]);
//# sourceMappingURL=chunk-6136713b.d0d2e488.js.map