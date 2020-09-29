import service from "./service.js";

export default {
  rootURL: "/binLocation",
  list() {
    let url = this.rootURL;
    return service.get(url);
  },
  getById(id) {
    let url = `${this.rootURL}/${id}`;
    return service.get(url);
  },
  create(obj) {
    let url = `${this.rootURL}`;
    return service.post(url, obj);
  },
  update(id, obj) {
    let url = `${this.rootURL}/${id}`;
    return service.put(url, obj);
  },
  async bulkDelete(ids) {
    console.log(ids);
    if (ids.length == 0) return;
    let errors = [];
    let idsUrl = ids.join(",");
    let url = `${this.rootURL}/BulkDelete/${idsUrl}`;

    try {
      await service.delete(url).then(a => console.log(a));
    } catch (error) {
      console.error(url, error);
      errors.push(error);
      throw errors;
    }
  },
  async delete(obj) {
    if (obj == null) {
      throw "Please select a record to Delete";
    }
    let errors = [];
    let url = "";
    try {
      url = `${this.rootURL}/${obj}`;
      await service.delete(url).then(a => console.log(a));
    } catch (error) {
      console.error(url, error);
      errors.push(error);
      throw errors;
    }
    var response = {};
    if (errors.length == 0) {
      response = {
        status: "200",
        statusText: `Deleting of "${obj.BinLocDesc}" Successful`
      };
    } else {
      response = {
        status: "404",
        statusText: `Failed to delete some of the selected records`,
        errors: errors
      };
    }
    return response;
  }
};
