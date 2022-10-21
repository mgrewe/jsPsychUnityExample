mergeInto(LibraryManager.library, {
  
  PublishEventJSPsych: function (eventJson) {
    window.dispatchEvent(new CustomEvent(
      "JSPsychUnityEvent", 
      {detail: UTF8ToString(eventJson)}
      )
    );
  },

  PublishEventJSPsychUnityReady: function () {
    window.dispatchEvent(new Event(
      "JSPsychUnityReadyEvent"
      )
    );
  },
  
  // Unity-bug workaound
  //
  // See https://forum.unity.com/threads/ionic-webgl-out-of-memory-on-quit.1244074/
  //
  // override default callback
  // to avoid error on removal of the
  // unity canvas from the DOM
  emscripten_set_wheel_callback_on_thread: function (
    target,
    userData,
    useCapture,
    callbackfunc,
    targetThread
  ) {
    target = findEventTarget(target);

    // the fix
    if (!target) {
      return -4;
    }

    if (typeof target.onwheel !== 'undefined') {
      registerWheelEventCallback(
        target,
        userData,
        useCapture,
        callbackfunc,
        9,
        'wheel',
        targetThread
      );
      return 0;
    } else {
      return -1;
    }
  },

});