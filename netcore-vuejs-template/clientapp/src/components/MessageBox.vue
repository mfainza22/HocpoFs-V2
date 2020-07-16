<template>
  <v-dialog v-model="visible" :max-width="options.maxWidth">
    <v-card>
      <v-card-title>{{ options.title }}</v-card-title>
      <v-card-text>{{ options.content }}</v-card-text>
      <v-divider />
      <v-card-actions class="children-mr-2 justify-end">
        <template v-if="options.buttons == enumsButtons.OK_CANCEL">
          <ButtonOk :loading="loading" @click="clickEvent(enumsEvents.OK)" />
          <ButtonCancel @click="clickEvent(enumsEvents.CANCEL)" />
        </template>
        <template v-else-if="options.buttons == enumsButtons.YES_NO">
          <ButtonYes :loading="loading" @click="clickEvent(enumsEvents.YES)" />
          <ButtonNo @click="clickEvent(enumsEvents.NO)" />
        </template>
        <template v-else-if="options.buttons == enumsButtons.SUBMIT_CANCEL">
          <ButtonSubmit
            :loading="loading"
            @click="clickEvent(enumsEvents.SUBMIT)"
          />
          <ButtonCancel @click="clickEvent(enumsEvents.CANCEL)" />
        </template>
        <template v-else-if="options.buttons == enumsButtons.RETRY_CANCEL">
          <ButtonOk :loading="loading" @click="clickEvent(enumsEvents.RETRY)" />
          <ButtonCancel @click="clickEvent(enumsEvents.CANCEL)" />
        </template>
        <template v-else-if="options.buttons == enumsButtons.DELETE_CANCEL">
          <ButtonDelete
            :loading="loading"
            @click="clickEvent(enumsEvents.DELETE)"
          />
          <ButtonCancel @click="clickEvent(enumsEvents.CANCEL)" />
        </template>
        <template v-else>
          <ButtonOk @click="clickEvent(enumsEvents.OK)" />
        </template>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
export default {
  name: "MessageBox",
  data() {
    return {
      options: {},
      visible: false,
      event: "cancel",
      resolve: null,
      reject: null,
      loading: false,
      callbackProceed: null,
      defaultOptions: {
        type: "default",
        title: "Title",
        content: "Mesagebox content",
        buttons: "ok",
        maxWidth: 500
      },
      enumsType: {
        DEFAULT: "default",
        SUCCESS: "success",
        WARNING: "warning",
        ERROR: "error"
      },
      enumsButtons: {
        OK: "ok",
        OK_CANCEL: "okCancel",
        YES_NO: "yesNo",
        SUBMIT_CANCEL: "submitCancel",
        RETRY_CANCEL: "retryCancel",
        DELETE_CANCEL: "deleteCancel",
        CUSTOM: "custom"
      },
      enumsEvents: {
        OK: "ok",
        CANCEL: "cancel",
        YES: "yes",
        NO: "no",
        RETRY: "retry",
        SUBMIT: "submit",
        DELETE: "delete",
        PROCEED: "proceed"
      }
    };
  },
  methods: {
    async open(options, callback) {
      this.callbackProceed = callback;
      Object.assign(this.options, this.defaultOptions, options);
      return await new Promise((resolve, reject) => {
        this.visible = true;
        this.resolve = resolve;
        this.reject = reject;
      });
    },
    show() {
      this.visible = true;
    },
    hide() {
      this.loading = false;
      this.visible = false;
    },
    async proceed() {
      this.loading = true;
      await this.callbackProceed();
      this.hide();
      return this.resolve({ result: true, event: this.event });
    },
    cancel() {
      this.hide();
      return this.resolve({ result: false, event: this.event });
    },
    clickEvent(evt) {
      this.event = evt;
      if (
        evt == this.enumsEvents.OK ||
        evt == this.enumsEvents.YES ||
        evt == this.enumsEvents.PROCEED ||
        evt == this.enumsEvents.SUBMIT ||
        evt == this.enumsEvents.DELETE
      ) {
        this.proceed();
      } else if (evt == this.enumsEvents.CANCEL || evt == this.enumsEvents.NO) {
        this.cancel();
      }
    }
  }
};
</script>

<style></style>
