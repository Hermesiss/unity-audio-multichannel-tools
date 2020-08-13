copy README.md "Packages/trismegistus.unity.audio-multichannel/ReadMe.md" /Y
copy CHANGELOG.md "Packages/trismegistus.unity.audio-multichannel/CHANGELOG.md" /Y
cd Packages/trismegistus.unity.audio-multichannel
npm publish --registry http://upm.trismegistus.tech:4873/ || pause