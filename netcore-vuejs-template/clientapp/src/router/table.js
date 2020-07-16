const routes = [
  {
    name: "BinLocations",
    path: "/binlocations",
    component: () => import("../views/tables/BinLocations"),
    children: [
      {
        name: "BinLocationCreate",
        path: "create",
        component: () => import("../views/tables/BinLocation.vue")
      },
      {
        name: "BinLocationUpdate",
        path: "update/:id",
        props: true,
        component: () => import("../views/tables/BinLocation.vue")
      }
    ]
  },
  {
    name: "PackagingTypes",
    path: "/packagingtypes",
    component: () => import("../views/tables/PackagingTypes"),
    children: [
      {
        name: "PackagingTypeCreate",
        path: "create",
        component: () => import("../views/tables/PackagingType.vue")
      },
      {
        name: "PackagingTypeUpdate",
        path: "update/:id",
        props: true,
        component: () => import("../views/tables/PackagingType.vue")
      }
    ]
  },
  {
    name: "Pallets",
    path: "/Pallets",
    component: () => import("../views/tables/Pallets"),
    children: [
      {
        name: "PalletCreate",
        path: "create",
        component: () => import("../views/tables/Pallet.vue")
      },
      {
        name: "PalletUpdate",
        path: "update/:id",
        props: true,
        component: () => import("../views/tables/Pallet.vue")
      }
    ]
  },
  {
    name: "RawMaterials",
    path: "/rawmaterials",
    component: () => import("../views/tables/RawMaterials"),
    children: [
      {
        name: "RawMaterialCreate",
        path: "create",
        component: () => import("../views/tables/RawMaterial.vue")
      },
      {
        name: "RawMaterialUpdate",
        path: "update/:id",
        props: true,
        component: () => import("../views/tables/RawMaterial.vue")
      }
    ]
  },
  {
    name: "Forklifts",
    path: "/forklifts",
    component: () => import("../views/tables/Forklifts"),
    children: [
      {
        name: "ForkliftCreate",
        path: "create",
        component: () => import("../views/tables/Forklift.vue")
      },
      {
        name: "ForkliftUpdate",
        path: "update/:id",
        props: true,
        component: () => import("../views/tables/Forklift.vue")
      }
    ]
  },
  {
    name: "Warehouses",
    path: "/warehouses",
    component: () => import("../views/tables/Warehouses"),
    children: [
      {
        name: "WarehouseCreate",
        path: "create",
        component: () => import("../views/tables/Warehouse.vue")
      },
      {
        name: "WarehouseUpdate",
        path: "update/:id",
        props: true,
        component: () => import("../views/tables/Warehouse.vue")
      }
    ]
  },
  {
    name: "Locations",
    path: "/Locations",
    component: () => import("../views/tables/Locations"),
    children: [
      {
        name: "LocationCreate",
        path: "create",
        component: () => import("../views/tables/Location.vue")
      },
      {
        name: "LocationUpdate",
        path: "update/:id",
        props: true,
        component: () => import("../views/tables/Location.vue")
      }
    ]
  },
  {
    name: "Shifts",
    path: "/Shifts",
    component: () => import("../views/tables/Shifts"),
    children: [
      {
        name: "ShiftCreate",
        path: "create",
        component: () => import("../views/tables/Shift.vue")
      },
      {
        name: "ShiftUpdate",
        path: "update/:id",
        props: true,
        component: () => import("../views/tables/Shift.vue")
      }
    ]
  }
];

export default routes;
