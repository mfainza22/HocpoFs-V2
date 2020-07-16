import Vue from "vue";
import VueRouter from "vue-router";
import errors from "./error";
import tables from "./table";

Vue.use(VueRouter);

const routes = [...tables, ...errors];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});

export default router;
