const jsonServer = require("json-server");
const server = jsonServer.create();
const router = jsonServer.router("db.json");
const middlewares = jsonServer.defaults();
const _ = require("lodash");
server.use(middlewares);
server.use(jsonServer.bodyParser);
server.use((req, res, next) => {
  const entity = req.path.split("/")[1];
  const resProp = getResource(entity);
  console.log(resProp);

  if (req.method == "POST") {
    // get new id
    const id = router.db
      .get(resProp.name)
      .createId()
      .value();

    // create keyfield in the request body
    req.body[resProp.key] = id;
  }
  next();
});

server.get(`/:resource/:id`, (req, res) => {
  const { resource, id: resourceId } = req.params;

  var resProp = getResource(resource);
  var query = getQueryId(resProp, resourceId);
  console.log(query);
  const result = router.db
    .get(resProp.name)
    .find(query)
    .value();

  return res.status(200).jsonp(result);
});

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

server.use(router);
server.listen(3000, () => {
  console.log("JSON Server is running");
});
