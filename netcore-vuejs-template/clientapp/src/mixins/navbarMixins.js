import { EventBus } from "@/main.js";
export default {
  mounted() {
    EventBus.$emit("setPageTitle", this.pageTitle);
  },
  watch: {
    pageTitle(val) {
      EventBus.$emit("setPageTitle", val);
    }
  }
};
