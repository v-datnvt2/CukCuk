<template>
  <div>
    <div class="popup warning" :class="{ isnone: isHiddenWarning }">
      <div class="header">
        <div class="title">
          <span class="title-text">
            <span class="title-target">Warning</span>
          </span>
        </div>
        <Button subclass="btn-close" @btn-click="$emit('callCloseForm')" />
        <!-- <div @click="$emit('btn-cancel')" class="btn-close todo"></div> -->
      </div>
      <div class="popup-content">
        <div class="warning-icon" :class="'icon-' + warningType"></div>
        <div class="warning-message">
          <span class="text-before">{{ popupMessage["text-before"] }} </span>
          <span class="text-target"> {{ popupMessage["text-target"] }}</span>
          <span class="text-after"> {{ popupMessage["text-after"] }} </span>
        </div>
      </div>
      <div class="footer">
        <Button
          :subclass="'btn-confirm ' + warningType"
          @btn-click="btnConfirmOnClick"
          :buttonText="buttonConfirmText"
        />
        <Button
          subclass="cancel"
          @btn-click="btnCancelOnClick"
          :buttonText="buttonCancelText"
        />
      </div>
    </div>
  </div>
</template>
<script>
import Button from "../../components/base/BaseButton.vue";
export default {
  name: "WarningPopup",
  props: {
    popupMessage: Object,
    action: String,
    isHiddenWarning: Boolean,
  },
  components: {
    Button,
  },
  data() {
    return {
      warningType: "",
      buttonConfirmText:"",
      buttonCancelText:""
    };
  },
  methods: {
    btnConfirmOnClick() {
      this.$emit("btn-confirm", this.action);
    },
    btnCancelOnClick() {
      this.$emit("btn-cancel", this.action);
    },
  },
  watch: {
    /**
     * watch kiểu của popup popupMessage, gán class tương ứng cho loại popup đó
     * các loại Danger/ Warning / Notify
     * NHH 30/07
     */
    isHiddenWarning: function () {
      this.warningType = this.popupMessage["warningType"].toLowerCase();

      if(this.popupMessage["buttonConfirmText"]){
        this.buttonConfirmText = this.popupMessage["buttonConfirmText"]
      }else{
         this.buttonConfirmText= "Xác nhận"
      }

      if(this.popupMessage["buttonCancelText"]){
        this.buttonConfirmText = this.popupMessage["buttonCancelText"]
      }else{
         this.buttonCancelText= "Hủy"
      }
      
    },
  },
};
</script>