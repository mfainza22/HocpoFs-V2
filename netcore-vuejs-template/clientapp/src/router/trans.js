import { enums } from "@/helpers/constants.js";
// var getTransactionProcess = routePath => {
//   const path = routePath.split("/")[1];
//   let r = enums.transactionProcess.NONE;
//   if (path == "weighin") {
//     r = enums.transactionProcess.WEIGH_IN;
//   } else if (path == "weighout") {
//     r = enums.transactionProcess.WEIGH_OUT;
//   } else if (path == "updatein") {
//     r = enums.transactionProcess.UPDATE_IN;
//   } else if (path == "updateout") {
//     r = enums.transactionProcess.UPDATE_OUT;
//   }
//   return r;
// };

const routes = [
  {
    name: "Transactions",
    path: "/transactions",
    component: () => import("../views/transactions/TransRecordList"),
    children: []
  },
  {
    name: "TransactionWeighIn",
    path: "/weighin",
    component: () => import("../views/transactions/TransRecord.vue"),
    props: true,
    meta: {
      transactionProcess: enums.transactionProcess.WEIGH_IN
    }
  },
  {
    name: "TransactionWeighOut",
    path: "/weighout/:id",
    component: () => import("../views/transactions/TransRecord.vue"),
    props: true,
    meta: {
      transactionProcess: enums.transactionProcess.WEIGH_OUT,
      transactionStatus: enums.transactionStatus.PENDING
    }
  },
  {
    name: "TransactionUpdateIn",
    path: "/updatein/:id",
    component: () => import("../views/transactions/TransRecord.vue"),
    props: true,
    meta: {
      transactionProcess: enums.transactionProcess.UPDATE_IN,
      transactionStatus: enums.transactionStatus.PENDING
    }
  },
  {
    name: "TransactionUpdateOut",
    path: "/updateout/:id",
    component: () => import("../views/transactions/TransRecord.vue"),
    props: true,
    meta: {
      transactionProcess: enums.transactionProcess.UPDATE_OUT,
      transactionStatus: enums.transactionStatus.COMPLETED
    }
  },
  {
    name: "TransactionDeleteIn",
    path: "/deletein/:id",
    component: () => import("../views/transactions/TransRecord.vue"),
    props: true,
    meta: {
      transactionProcess: enums.transactionProcess.DELETE_IN,
      transactionStatus: enums.transactionStatus.PENDING
    }
  },
  {
    name: "TransactionDeleteOut",
    path: "/deleteout/:id",
    component: () => import("../views/transactions/TransRecord.vue"),
    props: true,
    meta: {
      transactionProcess: enums.transactionProcess.DELETE_OUT,
      transactionStatus: enums.transactionStatus.PENDING
    }
  }
];

export default routes;
