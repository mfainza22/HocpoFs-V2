import warehouseService from "@/services/warehouseService.js";
import { objectUtil } from "@/helpers/func";

export const name = "warehouse";
export const namespaced = true;
export const state = {
  item: {},
  items: []
};

export const getters = {
  defaultItem: () => {
    return Object.freeze({
      WarehouseId: 0,
      WarehouseNum: ""
    });
  }
};

export const actions = {
  async handleError({ dispatch }, error) {
    const noti = {
      visible: true,
      type: "error",
      content: error,
      icon: "mdi-exclamation-thick"
    };
    dispatch("notification/show", noti, { root: true });
  },
  async handleSuccess({ dispatch }, content) {
    const noti = {
      visible: true,
      type: "success",
      content: content,
      icon: "mdi-check"
    };
    dispatch("notification/show", noti, { root: true });
  },
  async getWarehouse({ commit, dispatch, getters }, id) {
    try {
      let r = {};
      if (id == 0) {
        r = JSON.parse(JSON.stringify(getters.defaultItem));
      } else {
        r = await warehouseService.getById(id);
        r = r.data;
      }

      commit("STORE_WAREHOUSE", r);
    } catch (error) {
      dispatch("handleError", error);
    }
  },
  async listWarehouse({ commit, dispatch }, forceUpdate) {
    try {
      if (forceUpdate == true || objectUtil.empty(state.items)) {
        const r = await warehouseService.list();
        commit("STORE_WAREHOUSE_LIST", r.data);
      }
    } catch (error) {
      dispatch("handleError", error);
    }
  },
  async createWarehouse({ commit, dispatch }, obj) {
    try {
      const r = await warehouseService.create(obj);
      commit("ADD_WAREHOUSE", r.data);
      dispatch("handleSuccess", "New Warehouse added successfully");
    } catch (error) {
      dispatch("handleError", error);
    }
  },
  async updateWarehouse({ commit, dispatch }, obj) {
    try {
      const r = await warehouseService.update(obj.WarehouseId, obj);
      commit("UPDATE_WAREHOUSE", r.data);
      dispatch("handleSuccess", "Warehouse updated successfully");
    } catch (error) {
      dispatch("handleError", error);
    }
  },
  async deleteWarehouse({ commit, dispatch }, obj) {
    try {
      if (obj == null || obj.length == 0) {
        throw new Error("Please select a record to delete.");
      }

      let params = obj.map(a => a.WarehouseId);
      await warehouseService.bulkDelete(params);

      commit("DELETE_WAREHOUSES", obj);
      dispatch(
        "handleSuccess",
        `${params.length} ${
          params.length == 1 ? "record" : "records"
        } deleted successfully`
      );
    } catch (error) {
      dispatch("handleError", error);
    }
  }
};

export const mutations = {
  STORE_WAREHOUSE(state, payload) {
    state.item = payload;
  },
  STORE_WAREHOUSE_LIST(state, payload) {
    state.items = payload;
  },
  ADD_WAREHOUSE(state, payload) {
    state.items.push(payload);
  },
  UPDATE_WAREHOUSE(state, payload) {
    var si = state.items.find(a => a.WarehouseId == payload.WarehouseId);
    if (si == null) {
      state.items.push(payload);
    } else {
      const idx = state.items.indexOf(si);
      state.items.splice(idx, 1, payload);
    }
  },
  DELETE_WAREHOUSES(state, payload) {
    payload.forEach(item => {
      let idx = state.items.map(a => a.WarehouseId).indexOf(item.WarehouseId);
      console.log(idx);
      state.items.splice(idx, 1);
    });
  }
};
