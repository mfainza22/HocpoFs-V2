import service from "./service.js";
import params from "@/helpers/params";

export default {
  rootURL: "/transRecord",
  list(obj) {
    const data = params.objectToParams(obj);
    let url = this.rootURL + data;
    return service.get(url);
  },
  getPending(id) {
    let url = `${this.rootURL}/pending/${id}`;
    return service.get(url);
  },
  getCompleted(id) {
    let url = `${this.rootURL}/completed/${id}`;
    return service.get(url);
  },
  async weighIn(obj) {
    let url = `${this.rootURL}/weighin`;
    return await service.post(url, obj);
  },
  async weighOut(obj) {
    let url = `${this.rootURL}/weighout`;
    return await service.put(url, obj);
  },
  updatePending(id, obj) {
    let url = `${this.rootURL}/${id}`;
    return service.put(url, obj);
  },
  updateCompleted(id, obj) {
    let url = `${this.rootURL}/${id}`;
    return service.put(url, obj);
  },
  async bulkDelete(ids) {
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
        statusText: `Deleting of "${obj.ReceiptNum}" Successful`
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
