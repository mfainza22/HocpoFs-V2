const routes = [
  {
    name: "TransferLimits",
    path: "/TransferLimits",
    component: () => import("../views/management/TransferLimits"),
    children: [
      {
        name: "TransferLimitAdjs",
        path: "adjustments/:effectiveDate/:rawMaterialId/:shiftId",
        props: true,
        component: () => import("../views/management/TransferLimitAdjs"),
        children: [
          {
            name: "TransferLimitAdjCreate",
            path: "Create",
            props: true,
            component: () => import("../views/management/TransferLimitAdj.vue")
          },
          {
            name: "TransferLimitAdjUpdate",
            path: "Update/:id",
            props: true,
            component: () => import("../views/management/TransferLimitAdj.vue")
          }
        ]
      }
    ]
  }
];

export default routes;
