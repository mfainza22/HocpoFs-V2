module.exports = {
  css: {
    loaderOptions: {
      scss: {
        prependData:
          ' @import "@/scss/_variables.scss"; @import "@/assets/fonts/fonts.scss";  @import "@/scss/global.scss"; @import "@/scss/vuebar.scss";'
      }
    }
  },
  transpileDependencies: ["vuetify"]
};
