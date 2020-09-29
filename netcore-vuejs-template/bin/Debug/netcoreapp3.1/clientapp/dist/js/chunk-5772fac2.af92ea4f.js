(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-5772fac2"],{"50d5":function(e,t,r){"use strict";r("99af"),r("a15b"),r("96cf");var a=r("1da1"),n=r("3a78");t["a"]={rootURL:"/rawmaterial",list:function(){var e=this.rootURL;return n["a"].get(e)},getById:function(e){var t="".concat(this.rootURL,"/").concat(e);return n["a"].get(t)},create:function(e){var t="".concat(this.rootURL);return n["a"].post(t,e)},update:function(e,t){var r="".concat(this.rootURL,"/").concat(e);return n["a"].put(r,t)},bulkDelete:function(e){var t=this;return Object(a["a"])(regeneratorRuntime.mark((function r(){var a,o,c;return regeneratorRuntime.wrap((function(r){while(1)switch(r.prev=r.next){case 0:if(0!=e.length){r.next=2;break}return r.abrupt("return");case 2:return a=[],o=e.join(","),c="".concat(t.rootURL,"/BulkDelete/").concat(o),console.log(c),r.prev=6,r.next=9,n["a"].delete(c).then((function(e){return console.log(e)}));case 9:r.next=16;break;case 11:throw r.prev=11,r.t0=r["catch"](6),console.error(c,r.t0),a.push(r.t0),a;case 16:case"end":return r.stop()}}),r,null,[[6,11]])})))()},delete:function(e){var t=this;return Object(a["a"])(regeneratorRuntime.mark((function r(){var a,o,c;return regeneratorRuntime.wrap((function(r){while(1)switch(r.prev=r.next){case 0:if(null!=e){r.next=2;break}throw"Please select a record to Delete";case 2:return a=[],o="",r.prev=4,o="".concat(t.rootURL,"/").concat(e),r.next=8,n["a"].delete(o).then((function(e){return console.log(e)}));case 8:r.next=15;break;case 10:throw r.prev=10,r.t0=r["catch"](4),console.error(o,r.t0),a.push(r.t0),a;case 15:return c={},c=0==a.length?{status:"200",statusText:'Deleting of "'.concat(e.BinLocDesc,'" Successful')}:{status:"404",statusText:"Failed to delete some of the selected records",errors:a},r.abrupt("return",c);case 18:case"end":return r.stop()}}),r,null,[[4,10]])})))()}}},fcaf:function(e,t,r){"use strict";r.r(t);var a=function(){var e=this,t=e.$createElement,r=e._self._c||t;return r("div",[r("v-dialog",{attrs:{"max-width":"500",persistent:""},model:{value:e.show,callback:function(t){e.show=t},expression:"show"}},[r("ValidationObserver",{ref:"form",attrs:{tag:"form"},on:{submit:function(t){return t.preventDefault(),e.save(t)}}},[r("v-card",{staticClass:"pa-5"},[r("v-card-title",[r("span",[e._v(e._s(e.modalTitle)+" ")]),r("div",{directives:[{name:"show",rawName:"v-show",value:e.hasInternalError,expression:"hasInternalError"}]},[e._v(" "+e._s(e.internalErrorMsg)+" ")])]),r("v-card-text",[r("v-row",{attrs:{dense:""}},[r("v-col",{attrs:{cols:"12"}},[r("ValidationProvider",{attrs:{mode:"lazy",name:"Code",rules:{required:!0,max:50,remote:{url:"/rawmaterial/validatecode",obj:e.item}}},scopedSlots:e._u([{key:"default",fn:function(t){var a=t.errors;return[r("v-text-field",{attrs:{"error-messages":a,label:"Description"},model:{value:e.item.RawMaterialCode,callback:function(t){e.$set(e.item,"RawMaterialCode",t)},expression:"item.RawMaterialCode"}})]}}])}),r("ValidationProvider",{attrs:{mode:"lazy",name:"Description",rules:{required:!0,max:50,remote:{url:"/rawmaterial/validatedesc",obj:e.item}}},scopedSlots:e._u([{key:"default",fn:function(t){var a=t.errors;return[r("v-text-field",{attrs:{"error-messages":a,label:"Description"},model:{value:e.item.RawMaterialDesc,callback:function(t){e.$set(e.item,"RawMaterialDesc",t)},expression:"item.RawMaterialDesc"}})]}}])})],1)],1)],1),r("v-card-actions",{attrs:{fixed:""}},[r("v-spacer"),r("ButtonSave",{staticClass:"mr-2",attrs:{type:"submit"}}),r("ButtonCancel",{on:{click:function(t){return e.cancel()}}})],1)],1)],1)],1)],1)},n=[],o=(r("a9e3"),r("96cf"),r("1da1")),c=r("cca5"),i=r("50d5"),s=(r("6e40"),r("7bb1")),l={name:"RawMaterial",components:{ValidationProvider:s["b"],ValidationObserver:s["a"]},mixins:[c["a"]],props:{id:{type:Number,default:0}},data:function(){return{modalTitle:"",show:!0,item:{},internalErrorMsg:""}},computed:{hasInternalError:function(){return!!this.internalErrorMsg}},watch:{show:function(e){e||this.$router.back()}},created:function(){0==this.id?(this.item=this.defaultItem(),this.modalTitle="Add New Raw Material"):(this.item=this.getDetails(),this.modalTitle="Update Raw Material")},methods:{defaultItem:function(){return{BinLocationId:0,BinLocDesc:""}},save:function(){var e=this;this.$refs.form.validate().then((function(t){if(t)return 0==e.id?(e.cancelled=!1,e.create()):e.update()}))},create:function(){var e=this;return Object(o["a"])(regeneratorRuntime.mark((function t(){var r;return regeneratorRuntime.wrap((function(t){while(1)switch(t.prev=t.next){case 0:return r={visible:!0},t.prev=1,t.next=4,i["a"].create(e.item);case 4:r.type="success",r.content="New Raw Material added successfully",e.close(),t.next=14;break;case 9:t.prev=9,t.t0=t["catch"](1),r.type="error",e.internalErrorMsg=t.t0,r.content=t.t0;case 14:return t.prev=14,e.showNotification(r),t.finish(14);case 17:case"end":return t.stop()}}),t,null,[[1,9,14,17]])})))()},update:function(){var e=this;return Object(o["a"])(regeneratorRuntime.mark((function t(){var r;return regeneratorRuntime.wrap((function(t){while(1)switch(t.prev=t.next){case 0:return r={visible:!0},t.prev=1,t.next=4,i["a"].update(e.item.RawMaterialId,e.item);case 4:r.type="success",r.content="Raw Material updated successfully",e.close(),t.next=14;break;case 9:t.prev=9,t.t0=t["catch"](1),r.type="error",r.content=t.t0,e.internalErrorMsg=t.t0;case 14:return t.prev=14,e.showNotification(r),t.finish(14);case 17:case"end":return t.stop()}}),t,null,[[1,9,14,17]])})))()},cancel:function(){this.close()},close:function(){this.$router.back()},getDetails:function(){var e=this;return Object(o["a"])(regeneratorRuntime.mark((function t(){var r;return regeneratorRuntime.wrap((function(t){while(1)switch(t.prev=t.next){case 0:return t.prev=0,t.next=3,i["a"].getById(e.id);case 3:r=t.sent,e.item=r.data,t.next=10;break;case 7:t.prev=7,t.t0=t["catch"](0),console.log(t.t0);case 10:case"end":return t.stop()}}),t,null,[[0,7]])})))()}},beforeRouteLeave:function(e,t,r){this.$route.meta.cancelled=this.cancelled,r()}},u=l,d=r("2877"),f=r("6544"),v=r.n(f),h=r("b0af"),m=r("99d9"),p=r("62ad"),w=r("169a"),b=r("0fd9"),x=r("2fa4"),g=r("8654"),R=Object(d["a"])(u,a,n,!1,null,null,null);t["default"]=R.exports;v()(R,{VCard:h["a"],VCardActions:m["a"],VCardText:m["b"],VCardTitle:m["c"],VCol:p["a"],VDialog:w["a"],VRow:b["a"],VSpacer:x["a"],VTextField:g["a"]})}}]);
//# sourceMappingURL=chunk-5772fac2.af92ea4f.js.map