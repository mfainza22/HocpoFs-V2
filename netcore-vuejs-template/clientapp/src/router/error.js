export default [
  {
    path: "*",
    component: () => import("../views/errors/NotFound.vue")
  }
];
