const jsonServer = require("json-server");
const server = jsonServer.create();
const router = jsonServer.router("db.json");
const middlewares = jsonServer.defaults();
const _ = require("lodash");

const idMappings = {
  BinLocations: "BinLocationId",
  PackagingTypes: "PackagingTypeId",
  Pallets: "PalletId",
  RawMaterials: "RawMaterialId",
  ForkLifts: "ForkliftId",
  Warehouses: "WarehouseId",
  Locations: "LocationId",
  Shifts: "ShiftId"
};

// server.use(jsonServer.rewriter({
//   '/api/*': '/$1',
//   '/:resource/:id/': '/:resource/:id'
// }))

// router.render = (req, res) => {
//   const entity = req.path.split("/")[1];
//   let data;
//   if (_.isArray(res.locals.data)) {
//     data = _.map(res.locals.data, item => {
//       // item[idMappings[entity]] = `${item.id}`;
//       delete item.id;
//       return item;
//     });

//   } else {

//    console.log(router.db
//     .get("BinLocations?BinLocationId=1"));
//   }

//   res.jsonp(
//     data
//   );
// };

server.use(jsonServer.bodyParser);
server.use((req, res, next) => {
  if (req.method === "POST") {
    console.log("POST");
    const entity = req.path.split("/")[1];
    req.body.id = req.body[idMappings[entity]];

    const ids = router.db
      .get("BinLocations")
      .value()
      .map(a => a);
    console.log(ids);

    console.log(req.body);
  }
  // Continue to JSON Server router
  next();
});

server.get(`/:resource/:id`, (req, res) => {
  const { resource, id: resourceId } = req.params;

  var resourceKey = _.findKey(idMappings, (v, k) => {
    if (k.toLowerCase() == resource.toLowerCase()) {
      return k;
    }
  });
  const query =
    resourceKey in idMappings
      ? {
          [idMappings[resourceKey]]: parseInt(resourceId)
        }
      : {
          id: resourceId
        };

  console.log(query);
  console.log(
    router.db
      .get("BinLocations")
      .find(query)
      .value()
  );
  const result = router.db
    .get(resourceKey)
    .find(query)
    .value();

  return res.status(200).jsonp(result);
});

server.use(middlewares);
server.use(router);
server.listen(3000, () => {
  console.log("JSON Server is running");
});

// server.get(`/:resource/:id`, (req, res) => {
//   const { resource, id: resourceId } = req.params;
//   console.log(resourceId);
//   const query =
//     resource in idMappings
//       ? {
//           [idMappings[resource]]: resourceId
//         }
//       : {
//           id: resourceId
//         };
//         console.log(resource);
//   const db = _.map(router.db.get(resource))[0][resource];
//   console.log(_.map(router.db.get(resource))[0].BinLocations);
//   // _.map(router.db.get(resource), item => {
//   //   console.log(item);
//   // });

//   const result = router.db
//     .get(resource)
//     .find(query)
//     .value();

//   return res.status(200).jsonp(result);
// });
