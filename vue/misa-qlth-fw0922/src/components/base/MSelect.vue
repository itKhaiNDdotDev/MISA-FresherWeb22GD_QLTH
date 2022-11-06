<!-- Vue component -->
<template>
  <!-- <div>
    <label class="typo__label">Simple select / dropdown</label>
    <multiselect v-model="value" :options="options" :multiple="true" :close-on-select="false" :clear-on-select="false" :preserve-search="true" placeholder="Pick some" label="name" track-by="name" :preselect-first="true">
      <template slot="tag" slot-scope="props"><Your code></template>
    </multiselect>
    <pre class="language-json"><code>{{ value  }}</code></pre>
  </div> -->
  <div class="m-dropdown">
    <input
      @click="toggleMe"
      v-model="value"
      v-text="value"
      type="text"
      class="m-icon icon-down m-input"
      readonly
    />
    <div class="m-icon icon-down m-input tag-list">
      <div class="m-tag" v-for="(item, index) in selected" :key="index">
        <span class="m-tag-text">{{ item }}</span>
        <div class="m-icon m-icon-24 icon-x"></div>
      </div>
    </div>
    <div class="dropdown__option" :class="{ showOpt: isShow }">
      <div
        v-for="item in options"
        :key="item.id"
        class="dropdown__option--item"
        @click="chooseMe(item.id)"
      >
        <div class="item--check m-checkbox">
              <input class="checkbox-real" type="checkbox">
              <div class="checkbox-pseudo"></div>
        </div>
        <div class="item--text">{{item.name}}</div>
      </div>
    </div>
  </div>
</template>

<script>
// import Multiselect from 'vue-multiselect'
// // register globally
// Vue.component('multiselect', Multiselect)

export default {
  name: "MSelect",
  data() {
    return {
      isShow: false,
      value: [],
      options: [
        { id: "Vue.js", name: "JavaScript" },
        { id: "Adonis", name: "JavaScript" },
        { id: "Rails", name: "Ruby" },
        { id: "Sinatra", name: "Ruby" },
        { id: "Laravel", name: "PHP" },
        { id: "Phoenix", name: "Elixir" },
      ],
    };
  },
  methods: {
    toggleMe() {
      this.isShow = !this.isShow;
    },
    chooseMe(id) {
      this.value.push(id);
      console.log(this.value);
    },
  },
  computed: {
    selected() {
      var sltName = [];
      this.options.forEach(opt => {
        this.value.forEach( val => {
          if(opt.id == val)
            sltName.push(opt.name);
        });
      });
      return sltName;
    },
    
  },
};
</script>

<style scoped>
.dropdown__option.showOpt {
  display: block;
}
</style>