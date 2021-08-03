<template>
  <div>
    <div class="field-input-icon">
      <input
        type="text"
        :class="[subclass, iconName]"
        :id="id"
        :placeholder="placeHolder"
        :required="required == 'true' ? true : false"
        @input="onInput($event.target.value)"
        v-model="modelData"
      />
      <div
        class="clearchoice"
        :class="{ isnone: isEmpty }"
        @click="clearInputOnClick"
      ></div>
    </div>
  </div>
</template>
<script>
export default {
  name: "FieldInputIcon",
  data() {
    return {
      modelData: "",
      isEmpty: true,
    };
  },
  props: {
    subclass: String,
    iconName: String,
    id: String,
    value: String,
    fieldName: String,
    tabIndex: String,
    mydata: String,
    dataType: String,
    placeHolder: String,
    required: String,
  },
  methods: {
    /**
     * Khi input, emit sự kiện lên component cha cùng với giá trị hiện tại và tên của ô nhập dữ liệu
     * Nguyễn Hùng 30/07
     */
    onInput(value) {
      this.$emit("input", value);
      if (this.modelData.length > 0) this.isEmpty = false;
    },
    clearInputOnClick() {
      console.log("clearing");
      this.modelData = "";
      this.isEmpty = true;
    },
  },
  watch: {
    mydata: function () {
      try {
        this.modelData = this.mydata;
      } catch (error) {
        console.log("invalid data");
      }
    },
  },
};
</script>