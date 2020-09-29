import productService from "@/service/productService.js";

export default {
  name: "product",
  state: {
    products: [],
    product: {}
  },
  getters: {
    defaultItem: {
      id: 0,
      description: "",
      category: "",
      brand: "",
      serialCode: "",
      initStock: 0,
      onStock: 0,
      notes: "",
      img: "no-img.png"
    }
  },
  actions: {
    async createProduct({ dispatch }, item) {
      const noti = {
        type: "success",
        content: ""
      };

      try {
        await productService.create(item);
        noti.type = "success";
        noti.content = "New product added successfully";
      } catch (error) {
        noti.type = "error";
        this.internalErrorMsg = error;
        noti.content = error;
      } finally {
        dispatch("notification/show", null, noti);
      }
    },
    async updateProduct({ dispatch }, item) {
      const noti = {
        visible: true
      };

      try {
        await productService.update(item.id, item);
        noti.type = "success";
        noti.content = "Product updated successfully";
        this.close();
      } catch (error) {
        noti.type = "error";
        noti.content = error;
        this.internalErrorMsg = error;
      } finally {
        dispatch("notification/show", null, noti);
      }
    }
  },
  mutations: {
    STORE_PRODUCTS(state, payload) {
      state.products = payload;
    },
    ADD_PRODUCT(state, payload) {
      state.products.push(payload);
    },
    UPDATE_PRODUCT(state, payload) {
      payload.forEach(item => {
        let idx = state.receiving.items.map(a => a.id).indexOf(item.id);
        state.receiving.items.splice(idx, 1, item);
      });
    }
  }
};
