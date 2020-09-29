import tLimitAdjsService from "../../services/transferLimitAdjService";
import Vue from "vue";
export default {
  namespaced: true,
  state: {
    transferLimit: {},
    transferLimitStatus: {},
    items: [],
    item: {}
  },
  getters: {
    default() {
      return {
        TransferLimitAdjId: 0,
        TransferLimitId: 0,
        AdjLimit: 0,
        AdjRemarks: "",
        AdjCreatedById: ""
      };
    }
  },
  actions: {
    async getDetails({ commit }, obj) {
      try {
        const response = await tLimitAdjsService.list({
          effectiveDate: obj.effectiveDate,
          rawMaterialId: obj.rawMaterialId,
          shiftId: obj.shiftId
        });

        const {
          TransferLimitViewModel,
          TransferLimitStatus,
          TransferLimitAdjCollection
        } = response.data;
        commit("STORE_TRANSFERLIMIT", TransferLimitViewModel);
        commit("STORE_STATUS", TransferLimitStatus);
        commit("STORE_ADJS", TransferLimitAdjCollection);
      } catch (err) {
        console.log(err);
      }
    },
    async getAdj({ state }, id) {
      for (var i = 0; i <= state.items.length - 1; i++) {
        if (state.items[i].TransferLimitAdjId == id) {
          return state.item[i];
        }
      }
      return null;
    },
    async createAdj({ commit }, item) {
      await tLimitAdjsService.create(item);
      commit("UPDATE_ADJ", item);
    },
    async updateAdj({ commit }, item) {
      await tLimitAdjsService.update(item);
      commit("CREATE_ADJ", item);
    },
    async deleteAdjs({ commit }, items) {
      try {
        let params = items.map(a => a.TransferLimitAdjId);
        await tLimitAdjsService.bulkDelete(params);
        commit("DELETE_ADJS", items);
      } catch (error) {
        console.log(error);
      }
    }
  },
  mutations: {
    STORE_TRANSFERLIMIT(state, payload) {
      state.transferLimit = payload;
    },
    STORE_STATUS(state, payload) {
      state.transferLimitStatus = payload;
    },
    STORE_ADJS(state, payload) {
      state.items = payload;
    },
    CREATE_ADJ(state, payload) {
      Vue.set(state, "items", state.items.push(payload));
    },
    UPDATE_ADJ(state, payload) {
      const index = state.items.findIndex(item => item.id === payload.id);
      if (index !== -1) {
        Vue.set(state.items, index, payload);
      }
    },
    DELETE_ADJS(state, payload) {
      payload.forEach(item => {
        let idx = state.items.items.map(a => a.id).indexOf(item.id);
        state.items.splice(idx, 1);
      });
    }
  }
};
