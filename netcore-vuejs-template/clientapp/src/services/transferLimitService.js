import service from "./service.js";

export default {
  rootURL: "/transferlimit",
  async list(dt) {
    let url = `${this.rootURL}/list?dt=${dt}`;
    return await service.get(url);
  },
  getById(id) {
    let url = `${this.rootURL}/${id}`;
    return service.get(url);
  },
  create(obj) {
    let url = `${this.rootURL}`;
    return service.post(url, obj);
  },
  update(obj) {
    let url = `${this.rootURL}`;
    return service.put(url, obj);
  },
  checkLimit(obj) {
    let url = `${this.rootURL}/checklimit`;
    const r = service.post(url, obj);
    return r;
  }
};
