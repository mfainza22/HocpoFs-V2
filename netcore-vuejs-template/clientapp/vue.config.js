module.exports = {
  css: {
    loaderOptions: {
      scss: {
        prependData:
          ' @import "@/scss/_variables.scss";  @import "@/scss/global.scss"; @import "@/scss/vuebar.scss";'
      }
    }
  },
  transpileDependencies: ["vuetify"]
};
