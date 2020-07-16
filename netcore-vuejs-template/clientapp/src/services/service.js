import axios from "axios";

function logErr({ url, error }) {
  console.error("An error encounted while fetching from :" + url, error);
}

export default {
  api() {
    return axios.create({
      baseURL: "http://localhost:3000",
      withCredentials: false, // This is the default
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      },
      timeout: 4000
    });
  },
  get(url) {
    return this.api()
      .get(url)
      .then(response => {
        return response;
      })
      .catch(error => {
        logErr({ url, error });
        throw error;
      });
  },
  post(url, data) {
    return this.api()
      .post(url, data)
      .then(response => {
        return response;
      })
      .catch(error => {
        logErr({ url, error });
        throw error;
      });
  },
  put(url, data) {
    return this.api()
      .put(url, data)
      .then(response => {
        return response;
      })
      .catch(error => {
        logErr({ url, error });
        throw error;
      });
  },
  delete(url, config) {
    return this.api()
      .delete(url, config)
      .then(response => {
        return response;
      })
      .catch(error => {
        logErr({ url, error });
        throw error;
      });
  }
};
