const app = () => {
    const song = document.querySelector('.song');
    const play = document.querySelector('.play');
    const outline = document.querySelector('.moving-outline circle');
    const video = document.querySelector('.vid-container video');

    // class="sound-picker" の要素の子要素のボタンをすべて列挙する
    const sounds = document.querySelectorAll('.sound-picker button');

    const timeDisplay = document.querySelector('.time-display');
    const timeSelect = document.querySelectorAll('.time-select button');

    // SGVPathElement の関数らしい
    const outlineLength = outline.getTotalLength(); // 1359.759765625

    // デフォルト 10 分
    let fakeDuration = 600;

    outline.style.strokeDasharray = outlineLength;
    outline.style.strokeDashoffset = outlineLength;

    // 上で取得した全ボタン要素に対して click イベントを登録
    sounds.forEach(sound => {
        // アロー演算子と function で this の挙動が違うので注意
        sound.addEventListener('click', function() {
            song.src = this.getAttribute('data-sound');
            video.src = this.getAttribute('data-video');
            checkPlaying();
        });
    });

    // 再生/停止ボタン押下処理
    play.addEventListener('click', () => {
        checkPlaying();
    });

    // 上で取得した全ボタン要素に対して click イベントを登録
    timeSelect.forEach(option => {
        option.addEventListener('click', function() {
            fakeDuration = this.getAttribute('data-time');
            timeDisplay.textContent = `${Math.floor(fakeDuration / 60)}:${Math.floor(fakeDuration % 60)}`;
        });
    });

    // 再生/停止をトグル
    const checkPlaying = () => {
        if (song.paused) {
            song.play();
            video.play();
            play.src = './svg/pause.svg';
        } else {
            song.pause();
            video.pause();
            play.src = './svg/play.svg';
        }
    };

    // song.addEventListener('timeupdate', (e) =>
    // と多分一緒。
    // HTMLMediaElement の関数らしい。再生位置が変化した際に呼び出される。
    song.ontimeupdate = () =>
    {
        let currentTime = song.currentTime;
        let elapsed = fakeDuration - currentTime;
        let seconds = Math.floor(elapsed % 60);
        let minutes = Math.floor(elapsed / 60);

        let progress = outlineLength - (currentTime / fakeDuration) * outlineLength;
        outline.style.strokeDashoffset = progress;
        timeDisplay.textContent = `${minutes}:${seconds}`;

        // 最大再生時間を超えたら止める
        if (currentTime >= fakeDuration) {
            song.pause();
            song.currentTime = 0;
            play.src = './svg/play.svg';
            video.pause();
        }
    };
};

app();
