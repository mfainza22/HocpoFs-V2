import weighingAreaService from "@/services/weighingAreaService.js";
import { objectUtil } from "@/helpers/func";

export const name = "weighingArea";
export const namespaced = true;
export const state = {
  item: {},
  items: []
};

export const getters = {
  defaultItem: () => {
    return Object.freeze({
      WeighingAreaId: 0,
      WeighingAreaNum: ""
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
  async getWeighingArea({ commit, dispatch, getters }, id) {
    try {
      let r = {};
      if (id == 0) {
        r = JSON.parse(JSON.stringify(getters.defaultItem));
      } else {
        r = await weighingAreaService.getById(id);
        r = r.data;
      }

      commit("STORE_WEIGHINGAREA", r);
    } catch (error) {
      dispatch("handleError", error);
    }
  },
  async listWeighingArea({ commit, dispatch }, forceUpdate) {
    try {
      if (forceUpdate == true || objectUtil.empty(state.items)) {
        const r = await weighingAreaService.list();
        commit("STORE_WEIGHINGAREA_LIST", r.data);
      }
    } catch (error) {
      dispatch("handleError", error);
    }
  },
  async createWeighingArea({ commit, dispatch }, obj) {
    try {
      const r = await weighingAreaService.create(obj);
      commit("ADD_WEIGHINGAREA", r.data);
      dispatch("handleSuccess", "New WeighingArea added successfully");
    } catch (error) {
      dispatch("handleError", error);
    }
  },
  async updateWeighingArea({ commit, dispatch }, obj) {
    try {
      const r = await weighingAreaService.update(obj.WeighingAreaId, obj);
      commit("UPDATE_WEIGHINGAREA", r.data);
      dispatch("handleSuccess", "WeighingArea updated successfully");
    } catch (error) {
      dispatch("handleError", error);
    }
  },
  async deleteWeighingArea({ commit, dispatch }, obj) {
    try {
      if (obj == null || obj.length == 0) {
        throw new Error("Please select a record to delete.");
      }

      let params = obj.map(a => a.WeighingAreaId);
      await weighingAreaService.bulkDelete(params);

      commit("DELETE_WEIGHINGAREAS", obj);
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
  STORE_WEIGHINGAREA(state, payload) {
    state.item = payload;
  },
  STORE_WEIGHINGAREA_LIST(state, payload) {
    state.items = payload;
  },
  ADD_WEIGHINGAREA(state, payload) {
    state.items.push(payload);
  },
  UPDATE_WEIGHINGAREA(state, payload) {
    var si = state.items.find(a => a.WeighingAreaId == payload.WeighingAreaId);
    if (si == null) {
      state.items.push(payload);
    } else {
      const idx = state.items.indexOf(si);
      state.items.splice(idx, 1, payload);
    }
  },
  DELETE_WEIGHINGAREAS(state, payload) {
    payload.forEach(item => {
      let idx = state.items
        .map(a => a.WeighingAreaId)
        .indexOf(item.WeighingAreaId);
      console.log(idx);
      state.items.splice(idx, 1);
    });
  }
};
