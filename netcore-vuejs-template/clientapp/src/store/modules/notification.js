export default {
  namespaced: true,
  state: {
    notification: {}
  },
  getters: {
    default() {
      return {
        type: "default",
        content: "Notification Content"
      };
    }
  },
  actions: {
    show({ commit }, notification) {
      commit("SHOW", notification);
    }
  },
  mutations: {
    SHOW(state, notification) {
      state.notification = notification;
    }
  }
};
