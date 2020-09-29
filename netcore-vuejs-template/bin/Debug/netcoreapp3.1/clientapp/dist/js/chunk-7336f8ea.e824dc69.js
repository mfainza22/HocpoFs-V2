(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-7336f8ea"],{"0fcf":function(e,t,r){"use strict";r.r(t);var n=function(){var e=this,t=e.$createElement,r=e._self._c||t;return r("div",[r("v-dialog",{attrs:{"max-width":"500",persistent:""},model:{value:e.show,callback:function(t){e.show=t},expression:"show"}},[r("ValidationObserver",{ref:"form",on:{submit:function(t){return t.preventDefault(),e.save(t)}}},[r("v-card",{staticClass:"pa-5"},[r("v-card-title",[r("span",[e._v(e._s(e.modalTitle))]),r("div",{directives:[{name:"show",rawName:"v-show",value:e.hasInternalError,expression:"hasInternalError"}]},[e._v(e._s(e.internalErrorMsg))])]),r("v-card-text",[r("v-row",{attrs:{dense:""}},[r("v-col",{attrs:{cols:"12"}},[r("ValidationProvider",{attrs:{mode:"lazy",name:"Description",rules:{required:!0,max:50,remote:{url:"/forklift/ValidateForkLiftNum",obj:e.item}}},scopedSlots:e._u([{key:"default",fn:function(t){var n=t.errors;return[r("v-text-field",{attrs:{"error-messages":n,label:"Forklift Number"},model:{value:e.item.ForkliftNum,callback:function(t){e.$set(e.item,"ForkliftNum",t)},expression:"item.ForkliftNum"}})]}}])})],1),r("v-col",{attrs:{cols:"12"}},[r("ValidationProvider",{attrs:{mode:"lazy",name:"Updated Tare Wt.",rules:{between:{min:1e3,max:9e3}}},scopedSlots:e._u([{key:"default",fn:function(t){var n=t.errors;return[r("v-text-field",{attrs:{type:"number","error-messages":n,label:"Updated Tare Wt."},model:{value:e.item.UpdatedTareWt,callback:function(t){e.$set(e.item,"UpdatedTareWt",e._n(t))},expression:"item.UpdatedTareWt"}})]}}])})],1)],1)],1),r("v-card-actions",{attrs:{fixed:""}},[r("v-spacer"),r("ButtonSave",{staticClass:"mr-2",attrs:{type:"submit"},on:{click:e.save}}),r("ButtonCancel",{on:{click:function(t){return e.cancel()}}})],1)],1)],1)],1)],1)},a=[],o=(r("a9e3"),r("96cf"),r("1da1")),c=r("cca5"),s=r("7e09"),i=(r("6e40"),r("7bb1")),u={name:"Forklift",components:{ValidationProvider:i["b"],ValidationObserver:i["a"]},mixins:[c["a"]],props:{id:{type:Number,default:0}},data:function(){return{modalTitle:"",show:!0,item:{},internalErrorMsg:""}},computed:{hasInternalError:function(){return!!this.internalErrorMsg}},watch:{show:function(e){e||this.$router.back()}},created:function(){0==this.id?(this.item=this.defaultItem(),this.modalTitle="Add New Forklift"):(this.item=this.getDetails(),this.modalTitle="Update Forklift")},beforeCreate:function(){},methods:{defaultItem:function(){return{ForkliftId:0,ForkliftNum:"",UpdatedTareWt:0}},save:function(){var e=this;return Object(o["a"])(regeneratorRuntime.mark((function t(){return regeneratorRuntime.wrap((function(t){while(1)switch(t.prev=t.next){case 0:e.$refs.form.validate().then((function(t){if(t)return 0==e.id?(e.cancelled=!1,e.create()):e.update()}));case 1:case"end":return t.stop()}}),t)})))()},create:function(){var e=this;return Object(o["a"])(regeneratorRuntime.mark((function t(){var r;return regeneratorRuntime.wrap((function(t){while(1)switch(t.prev=t.next){case 0:return r={visible:!0},t.prev=1,t.next=4,s["a"].create(e.item);case 4:r.type="success",r.content="New Forklift added successfully",e.close(),t.next=14;break;case 9:t.prev=9,t.t0=t["catch"](1),r.type="error",e.internalErrorMsg=t.t0,r.content=t.t0;case 14:return t.prev=14,e.showNotification(r),t.finish(14);case 17:case"end":return t.stop()}}),t,null,[[1,9,14,17]])})))()},update:function(){var e=this;return Object(o["a"])(regeneratorRuntime.mark((function t(){var r;return regeneratorRuntime.wrap((function(t){while(1)switch(t.prev=t.next){case 0:return r={visible:!0},t.prev=1,t.next=4,s["a"].update(e.item.ForkliftId,e.item);case 4:r.type="success",r.content="Forklift updated successfully",e.close(),t.next=14;break;case 9:t.prev=9,t.t0=t["catch"](1),r.type="error",r.content=t.t0,e.internalErrorMsg=t.t0;case 14:return t.prev=14,e.showNotification(r),t.finish(14);case 17:case"end":return t.stop()}}),t,null,[[1,9,14,17]])})))()},cancel:function(){this.cancelled=!0,this.close()},close:function(){this.show=!1},getDetails:function(){var e=this;return Object(o["a"])(regeneratorRuntime.mark((function t(){var r;return regeneratorRuntime.wrap((function(t){while(1)switch(t.prev=t.next){case 0:return t.prev=0,t.next=3,s["a"].getById(e.id);case 3:r=t.sent,e.item=r.data,t.next=10;break;case 7:t.prev=7,t.t0=t["catch"](0),console.log(t.t0);case 10:case"end":return t.stop()}}),t,null,[[0,7]])})))()}},beforeRouteLeave:function(e,t,r){this.$route.meta.cancelled=this.cancelled,r()}},l=u,d=r("2877"),f=r("6544"),p=r.n(f),m=r("b0af"),h=r("99d9"),v=r("62ad"),w=r("169a"),b=r("0fd9"),k=r("2fa4"),x=r("8654"),g=Object(d["a"])(l,n,a,!1,null,null,null);t["default"]=g.exports;p()(g,{VCard:m["a"],VCardActions:h["a"],VCardText:h["b"],VCardTitle:h["c"],VCol:v["a"],VDialog:w["a"],VRow:b["a"],VSpacer:k["a"],VTextField:x["a"]})},"7e09":function(e,t,r){"use strict";r("99af"),r("a15b"),r("96cf");var n=r("1da1"),a=r("3a78");t["a"]={rootURL:"/forklift",list:function(){var e=this.rootURL;return a["a"].get(e)},getById:function(e){var t="".concat(this.rootURL,"/").concat(e);return a["a"].get(t)},create:function(e){var t="".concat(this.rootURL);return a["a"].post(t,e)},update:function(e,t){var r="".concat(this.rootURL,"/").concat(e);return a["a"].put(r,t)},bulkDelete:function(e){var t=this;return Object(n["a"])(regeneratorRuntime.mark((function r(){var n,o,c;return regeneratorRuntime.wrap((function(r){while(1)switch(r.prev=r.next){case 0:if(0!=e.length){r.next=2;break}return r.abrupt("return");case 2:return n=[],o=e.join(","),c="".concat(t.rootURL,"/BulkDelete/").concat(o),console.log(c),r.prev=6,r.next=9,a["a"].delete(c).then((function(e){return console.log(e)}));case 9:r.next=16;break;case 11:throw r.prev=11,r.t0=r["catch"](6),console.error(c,r.t0),n.push(r.t0),n;case 16:case"end":return r.stop()}}),r,null,[[6,11]])})))()},delete:function(e){var t=this;return Object(n["a"])(regeneratorRuntime.mark((function r(){var n,o,c;return regeneratorRuntime.wrap((function(r){while(1)switch(r.prev=r.next){case 0:if(null!=e){r.next=2;break}throw"Please select a record to Delete";case 2:return n=[],o="",r.prev=4,o="".concat(t.rootURL,"/").concat(e),r.next=8,a["a"].delete(o).then((function(e){return console.log(e)}));case 8:r.next=15;break;case 10:throw r.prev=10,r.t0=r["catch"](4),console.error(o,r.t0),n.push(r.t0),n;case 15:return c={},c=0==n.length?{status:"200",statusText:'Deleting of "'.concat(e.BinLocDesc,'" Successful')}:{status:"404",statusText:"Failed to delete some of the selected records",errors:n},r.abrupt("return",c);case 18:case"end":return r.stop()}}),r,null,[[4,10]])})))()}}}}]);
//# sourceMappingURL=chunk-7336f8ea.e824dc69.js.map