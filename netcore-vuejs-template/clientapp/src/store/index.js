import Vue from "vue";
import Vuex from "vuex";

// import receiving from "./modules/receiving";
import notification from "./modules/notification";
import loaderSpinner from "./modules/loaderSpinner";
import transferLimit from "./modules/transferLimit";
import transferLimitAdj from "./modules/transferLimitAdj";
import * as transRecord from "./modules/transRecord";
import * as rawMaterial from "./modules/rawMaterial";
import * as packagingType from "./modules/packagingType";
import * as binLocation from "./modules/binLocation";
import * as pallet from "./modules/pallet";
import * as warehouse from "./modules/warehouse";
import * as weighingArea from "./modules/weighingArea";
Vue.use(Vuex);

export default new Vuex.Store({
  state: {},
  mutations: {},
  actions: {},
  modules: {
    notification,
    loaderSpinner,
    rawMaterial,
    packagingType,
    binLocation,
    pallet,
    warehouse,
    weighingArea,
    transferLimit,
    transferLimitAdj,
    transRecord
  }
});
