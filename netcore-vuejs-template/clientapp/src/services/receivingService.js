import service from "./service.js";
// var moment = require("moment");
var moment = require("moment-timezone");

export default {
  rootURL: "/receiving",
  list() {
    console.log(moment.tz);
    let url = this.rootURL;
    return service.get(url);
  },
  getById(id) {
    let url = `${this.rootURL}/${id}`;
    return service.get(url);
  },
  create(obj) {
    let url = this.rootURL;
    obj.tDate = moment(new Date()).format("Y-MM-DD");
    obj.tTime = moment(new Date()).format("HH:mm:ss Z");
    console.log(moment.tz("Asia/Shanghai"));
    return service.post(url, obj);
  },
  update(obj) {
    let url = `${this.rootURL}/${obj.id}`;
    return service.put(url, obj);
  },
  async remove(ids) {
    if (ids.length == 0) return;
    let errors = [];
    let url = "";
    for (let i = 0; i <= ids.length - 1; i++) {
      try {
        url = `${this.rootURL}/${ids[i]}`;
        await service.delete(url).then(a => console.log(a));
      } catch (error) {
        console.error(url, error);
        errors.push(error);
      }
    }

    var response = {};
    if (errors.length == 0) {
      response = {
        status: "200",
        statusText: `Deleting of ${ids.length} Receiving/s Successful`
      };
    } else {
      response = {
        status: "404",
        statusText: `Failed to delete some of the selected records`,
        errors: errors
      };
    }
    return response;
  },
  async getNew() {
    // let url = this.rootURL;
    // const response = await service.get(url);
    // const items = response.data;
    // for (let i = 0; i <= items.length - 1; i++) {}
  }
};
