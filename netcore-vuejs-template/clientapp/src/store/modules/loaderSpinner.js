export default {
  namespaced: true,
  state: {
    isLoading: false
  },
  getters: {},
  actions: {
    showSpinner({ commit }) {
      commit("TOGGLE_VISIBILITY", true);
    },
    hideSpinner({ commit }) {
      commit("TOGGLE_VISIBILITY", false);
    }
  },
  mutations: {
    TOGGLE_VISIBILITY(state, isLoading) {
      state.isLoading = isLoading;
    }
  }
};
