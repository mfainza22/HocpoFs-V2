import Vue from "vue";
import Vuex from "vuex";

// import receiving from "./modules/receiving";
import notification from "./modules/notification";
import receiving from "./modules/receiving";
import modalstatus from "./modules/modalstatus";
Vue.use(Vuex);

export default new Vuex.Store({
  state: {},
  mutations: {},
  actions: {},
  modules: { notification, modalstatus, receiving }
});
