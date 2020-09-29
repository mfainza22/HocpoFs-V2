import Vue from "vue";
import VueRouter from "vue-router";
import errors from "./error";
import tables from "./table";
import mgmt from "./mgmt";
import trans from "./trans";

Vue.use(VueRouter);

const routes = [...tables, ...mgmt, ...trans, ...errors];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});

export default router;
