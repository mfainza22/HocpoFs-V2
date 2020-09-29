import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";

import VueSticky from "vue-sticky-js";
Vue.use(VueSticky.install);

import Vuebar from "vuebar";
Vue.use(Vuebar);

Vue.config.productionTip = false;

Vue.use(require("vue-moment"));

import upperFirst from "lodash/upperFirst";
import camelCase from "lodash/camelCase";
import vuetify from "./plugins/vuetify";
const requireComponent = require.context(
  "./components",
  true,
  /[A-Z]\w+\.(vue|js)$/
);

requireComponent.keys().forEach(fileName => {
  const componentConfig = requireComponent(fileName);
  const componentName = upperFirst(
    camelCase(
      fileName
        .split("/")
        .pop()
        .replace(/\.\w+$/, "")
    )
  );
  Vue.component(componentName, componentConfig.default || componentConfig);
});

// Creating event bus for global emit of methods outside component
export const EventBus = new Vue();

Vue.config.productionTip = false;

new Vue({
  mode: "history",
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount("#app");
