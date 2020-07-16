import service from "./service.js";

export default {
  rootURL: "/customers",
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
  async delete(ids) {
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
        statusText: `Deleting of ${ids.length} Customer/s Successful`
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
  update(id, obj) {
    let url = `${this.rootURL}/${id}`;
    return service.put(url, obj);
  }
};
