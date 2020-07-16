const jsonServer = require("json-server");
const server = jsonServer.create();
const router = jsonServer.router("db.json", { _isFake: true });
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

const getResource = entity => {
  var key = _.findKey(idMappings, (v, k) => {
    if (k.toLowerCase() == entity.toLowerCase()) {
      return true;
    }
  });
  return { name: key, key: idMappings[key] };
};

const getQueryId = (resource, id) => {
  const { name } = resource;
  const query =
    name in idMappings
      ? {
          [idMappings[name]]: parseInt(id)
        }
      : {
          id: id
        };
  return query;
};

var getLastId = resource => {
  var maxId = 0;
  router.db
    .get(resource.name)
    .value()
    .map(a => {
      if (a[resource.key] > maxId) maxId = a[resource.key];
    });
  return maxId + 1;
};

server.use(jsonServer.bodyParser);
// server.use((req, res, next) => {
//   if (req.method === "POST") {
//     console.log("POST");
//     const entity = req.path.split("/")[1];
//     const resource = getResource(entity);
//     const newId = getLastId(resource);

//     // var id = { id: newId };
//     // var data = { ...req.body, ...id };
//     // router.db
//     //   .get(resource.name)
//     //   .insert(data)
//     //   .value();

//     // res.setHeader("Access-Control-Expose-Headers", "Location");
//     // res.locals.data = resource;
//     // res.status(201);
//   }

//   next();
// });

server.get(`/:resource/:id`, (req, res) => {
  const { resource, id: resourceId } = req.params;

  var resourceKey = getResource(resource);
  var query = getQueryId(resourceKey, resourceId);

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
