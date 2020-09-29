import axios from "axios";
import store from "@/store/index";
function logErr({ url, error }) {
  if (process.env.NODE_ENV == "development")
    console.error("An error encounted while fetching from :" + url, error);
}

export default {
  api() {
    const api = axios.create({
      baseURL: `${window.origin}/api`,
      withCredentials: false, // This is the default
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      },
      timeout: process.env.NODE_ENV ? 20000 : 10000
    });

    if (process.env.NODE_ENV == "development") {
      api.interceptors.request.use(
        request => {
          console.log("Starting Request", request);
          return request;
        },
        error => {
          console.log("Request Error:", error);
        }
      );

      api.interceptors.response.use(
        response => {
          console.log("Response:", response);
          return response;
        },
        error => {
          console.log("Response Error:", error.response);
          throw error;
        }
      );
    }

    return api;
  },
  async get(url, obj) {
    try {
      store.dispatch("loaderSpinner/showSpinner");
      return await this.api().get(url, obj);
    } catch (error) {
      logErr({ url, error });
      throw error;
    } finally {
      store.dispatch("loaderSpinner/hideSpinner");
    }
  },
  async post(url, data) {
    try {
      const response = await this.api().post(url, data);
      return response;
    } catch (error) {
      logErr({ url, error });
      throw error;
    } finally {
      store.dispatch("loaderSpinner/hideSpinner");
    }
  },
  async put(url, data) {
    try {
      return await this.api().put(url, data);
    } finally {
      store.dispatch("loaderSpinner/hideSpinner");
    }
  },
  async delete(url, data) {
    try {
      const response = await this.api().delete(url, data);
      return response;
    } catch (error) {
      logErr({ url, error });
      throw error;
    } finally {
      store.dispatch("loaderSpinner/hideSpinner");
    }
  }
};
